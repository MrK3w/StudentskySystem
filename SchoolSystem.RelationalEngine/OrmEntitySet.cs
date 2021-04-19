using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SchoolSystem.RelationalEngine
{
    public class OrmEntitySet<TEntity> where TEntity : class
    {
        private readonly SqlConnection _sqlConnection;

        internal List<EntityJoinRequest> EntityJoinRequests
        {
            get;
        }

        public OrmEntitySet(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Broken || sqlConnection.State == ConnectionState.Closed)
                throw new Exception("The SQL connection has to be opened.");

            _sqlConnection = sqlConnection;
            EntityJoinRequests = new List<EntityJoinRequest>();
        }

        public IEnumerable<TEntity> Get()
        {
            return Select();
        }

        private IEnumerable<TEntity> Select()
        {
            var entityType = typeof(TEntity);

            var entityAlias = TableEntityAliasCache.GetOrAddAlias(entityType);

            var tableName = GetTableName(entityType);

            var entityProperties = entityType.GetProperties();


            var columnNames = (from entityProperty in entityProperties 
                where entityProperty.GetCustomAttribute(typeof(ForeignKeyAttribute)) == null 
                select GetColumnName(entityProperty, true) into column 
                where column != null select $"{entityAlias}.{column}").ToList();

            LoadColumnsFromJoinedEntities(columnNames);
            var leftJoins = LoadLeftJoins();

            var sqlStatement = EntityJoinRequests.Count == 0 ? Statements.CreateSelectStatement(columnNames, tableName, ConditionSql) : Statements.CreateSelectWithJoinsStatement(columnNames, tableName, leftJoins, ConditionSql);


            return GetEntities(sqlStatement, columnNames, entityType, entityProperties);
        }

        private string LoadLeftJoins()
        {
            var stringBuilder = new StringBuilder();

            EntityJoinRequests.ForEach(request =>
            {
                var entityTypeFrom = request.EntityTypeFrom;
                var entityTypeTo = request.EntityTypeTo;
                var entityAliasFrom = TableEntityAliasCache.GetOrAddAlias(entityTypeFrom);
                var entityAliasTo = TableEntityAliasCache.GetOrAddAlias(entityTypeTo);
                var entityTableName = GetTableName(entityTypeFrom);

                var rightJoinAttribute = entityAliasFrom + "." + request.PrimaryKeyColumn;
                var leftJoinAttribute = entityAliasTo + "." + request.ForeignKeyColumn;

                stringBuilder.Append($"LEFT OUTER JOIN {entityTableName} ON ({leftJoinAttribute} = {rightJoinAttribute}) ");

            });

            return stringBuilder.ToString();
        }

        private void LoadColumnsFromJoinedEntities(ICollection<string> columnNames)
        {
            EntityJoinRequests.ForEach(request =>
            {
                var entityType = request.EntityTypeFrom;
                var entityProperties = entityType.GetProperties();
                var entityAlias = TableEntityAliasCache.GetOrAddAlias(entityType);

                entityProperties.ToList().ForEach(currentProperty =>
                {
                    if (currentProperty.GetCustomAttribute(typeof(ForeignKeyAttribute)) != null)
                        return;

                    var column = GetColumnName(currentProperty, true);
                    if (column != null) columnNames.Add($"{entityAlias}.{column}");
                });
            });
        }


        private IEnumerable<TEntity> GetEntities(string sqlStatement, IReadOnlyList<string> columnNames, Type entityType,
            PropertyInfo[] entityProperties)
        {
            var currentEntityAlias = TableEntityAliasCache.GetOrAddAlias(entityType);
            var propertiesColumnHashMap = new Dictionary<string, object>();
            var entities = new List<TEntity>();
            using var sqlCommand = new SqlCommand(sqlStatement, _sqlConnection);
            using var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                for (var index = 0; index < columnNames.Count; index++)
                {
                    propertiesColumnHashMap.Add(columnNames[index], sqlReader.GetValue(index));
                }

                var entity = Activator.CreateInstance(entityType);

                foreach (var entityProperty in entityProperties)
                {
                    var currentColumnName = currentEntityAlias + "." + GetColumnName(entityProperty, true);

                    if (MapInnerEntity(entityProperty, entity, propertiesColumnHashMap)) continue;

                    entityProperty.SetValue(entity,
                        propertiesColumnHashMap[currentColumnName] == DBNull.Value
                            ? null
                            : propertiesColumnHashMap[currentColumnName]);
                }

                entities.Add((TEntity)entity);

                propertiesColumnHashMap.Clear();
            }

            return entities;
        }

        private bool MapInnerEntity(PropertyInfo entityProperty, object entity, Dictionary<string, object> propertiesColumnHashMap)
        {
            if (entityProperty.GetCustomAttribute(typeof(ForeignKeyAttribute)) == null) return false;
            if (EntityJoinRequests.Count(request =>
                request.EntityTypeFrom == entityProperty.PropertyType) == 0)
            {
                entityProperty.SetValue(entity, null);
                return true;
            }

            var innerEntity = Activator.CreateInstance(entityProperty.PropertyType);


            foreach (var propertyInfo in entityProperty.PropertyType.GetProperties().ToList())
            {
                var innerEntityColumn = TableEntityAliasCache.GetOrAddAlias(entityProperty.PropertyType) +
                                        "." +
                                        GetColumnName(propertyInfo, true);
                if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute)) != null)
                {
                    MapInnerEntity(propertyInfo, innerEntity, propertiesColumnHashMap);
                }

                if (propertiesColumnHashMap.ContainsKey(innerEntityColumn))
                {
                    propertyInfo.SetValue(innerEntity,
                        propertiesColumnHashMap[innerEntityColumn] == DBNull.Value
                            ? null
                            : propertiesColumnHashMap[innerEntityColumn]);
                }
            }

            entityProperty.SetValue(entity, innerEntity);
            return true;

        }

        public void Delete()
        {
            var entityType = typeof(TEntity);
            var tableAlias = TableEntityAliasCache.GetOrAddAlias(entityType);
            var tableName = GetTableName(entityType);
            try
            {
                ExecuteNonQueryCommand(Statements.CreateDeleteStatement(tableAlias, ConditionSql, tableName));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ExecuteNonQueryCommand(string sqlStatement)
        {
            using var sqlCommand = new SqlCommand(sqlStatement, _sqlConnection);
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Insert(object entity)
        {
            var entityType = typeof(TEntity);

            var tableName = GetTableName(entityType, true);

            var props = entityType.GetProperties();

            StringBuilder columns = new StringBuilder().Append("(");
            StringBuilder values = new StringBuilder().Append("(");

            foreach (var prop in props)
            {
                PrepareInsertArguments(entity, prop, values, columns);
            }

            columns.Remove(columns.Length - 1, 1).Append(')');
            values.Remove(values.Length - 1, 1).Append(')');
            try
            {
                ExecuteNonQueryCommand(Statements.CreateInsertStatement(tableName, columns.ToString(), values.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(object entity)
        {
            var entityType = typeof(TEntity);

            var tableAlias = TableEntityAliasCache.GetOrAddAlias(entityType);

            var tableName = GetTableName(entityType);
            var props = entityType.GetProperties();
            StringBuilder arguments = new StringBuilder();
            foreach (var prop in props)
            {
                PrepareUpdateArguments(entity, prop, arguments);
            }

            arguments.Remove(arguments.Length - 1, 1);

            var updateSql = Statements.CreateUpdateStatement(tableAlias, tableName, arguments.ToString(), ConditionSql);
            
            try
            {
                ExecuteNonQueryCommand(updateSql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void PrepareInsertArguments(object entity, PropertyInfo prop, StringBuilder values,
            StringBuilder columns)
        {
            var column = GetColumnName(prop);
            if (column == null) return;
            if (prop.GetValue(entity) == null) values.Append("NULL,");
            else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
            {
                var innerProperties = prop.PropertyType.GetProperties();
                var instance = prop.GetValue(entity);
                var innerValue = GetInnerId(innerProperties, instance);
                if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(TimeSpan))
                {
                    values.Append($"'{innerValue}',");
                }
                else if (prop.PropertyType == typeof(string))
                {
                    
                }
                else
                {
                    values.Append($"{innerValue},");
                }
            }

            else
            {
                if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(TimeSpan))
                {
                    values.Append($"'{prop.GetValue(entity)}',");
                }
                else if(prop.PropertyType.IsEnum)
                {
                    values.Append($"{(int)prop.GetValue(entity)},");
                }

                else
                {
                    values.Append($"{prop.GetValue(entity)},");
                }
            }

            columns.Append(column + ",");
        }

        private void PrepareUpdateArguments(object entity, PropertyInfo prop, StringBuilder values)
        {
            var tableAlias = TableEntityAliasCache.GetOrAddAlias(typeof(TEntity));
            var column = GetColumnName(prop);
            if (column == null) return;
            if (prop.GetValue(entity) == null) values.Append($"{GetColumnName(prop)} = NULL,");
            else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
            {
                var innerProperties = prop.PropertyType.GetProperties();
                var instance = prop.GetValue(entity);
                var innerValue = GetInnerId(innerProperties, instance);
                if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(TimeSpan))
                {
                    values.Append($"{tableAlias}.{column} = '{innerValue}',");
                }
                else
                {
                    values.Append($"{tableAlias}.{column} = {innerValue},");
                }

            }
            else
            {
                if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(TimeSpan))
                {
                    values.Append($"{tableAlias}.{column} = '{prop.GetValue(entity)}',");
                }
                else if (prop.PropertyType.IsEnum)
                {
                    values.Append($"{tableAlias}.{column} = {(int)prop.GetValue(entity)},");
                }

                else
                {
                    values.Append($"{tableAlias}.{column} = {prop.GetValue(entity)},");
                }
            }
        }

        private object GetInnerId(IEnumerable<PropertyInfo> innerProperties,object obj)
        {
            foreach (var innerProperty in innerProperties)
            {
                var attributes = innerProperty.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is KeyAttribute)
                    {
                        return innerProperty.GetValue(obj);
                    }
                }
            }

            return default;
        }

        private string GetColumnName(PropertyInfo entityProperty, bool getId = false)
        {
            var propertyAttributes = entityProperty.GetCustomAttributes();

            foreach (var propertyAttribute in propertyAttributes)
            {
                switch (propertyAttribute)
                {
                    case KeyAttribute _ when getId == false:
                        return default;
                    case ColumnAttribute columnAttribute:
                        return columnAttribute.Name;
                }
            }

            return entityProperty.Name;
        }

        private string GetTableName(Type entityType, bool withoutAlias = false)
        {
            var attribute = entityType.GetCustomAttribute(typeof(TableAttribute));
            
            if (attribute == null)
                throw new Exception("The entity has no TableAttribute, therefore cannot create the SQL statement.");

            var tableAttribute = attribute as TableAttribute;
            if (!withoutAlias)
                return tableAttribute?.Schema + "." + tableAttribute?.Name + " " +
                       TableEntityAliasCache.GetOrAddAlias(entityType);
            return tableAttribute?.Schema + "." + tableAttribute?.Name;
        }

        public string ConditionSql { get; set; }
    }
}