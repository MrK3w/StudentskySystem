using System.Collections.Generic;

namespace SchoolSystem.RelationalEngine
{
    public static class Statements
    {
        public static string CreateUpdateStatement(string tableAlias, string tableName, string arguments, string condition)
        {
            return $"UPDATE {tableAlias} SET {arguments} FROM {tableName} {condition}";
        }
        
        public static string CreateInsertStatement(string tableName,string columns,string values)
        {
            return $"INSERT INTO " + tableName + columns + "VALUES"+values;
        }
        
        public static string CreateSelectStatement(List<string> columns, string tableName, string condition)
        {
            return "SELECT " + string.Join(", ", columns) + " FROM " + tableName + " " + condition;
        }
        
        public static string CreateDeleteStatement(string tableAlias,string condition, string tableName)
        {
            var xd = $"DELETE {tableAlias} FROM {tableName} {condition}";
            return xd;
        }

        public static string CreateSelectWithJoinsStatement(List<string> columnNames, string tableName, string leftJoins, string conditionSql)
        {
            return "SELECT " + string.Join(", ", columnNames) + " FROM " + tableName + " " + leftJoins + " " + conditionSql;
        }
    }
}