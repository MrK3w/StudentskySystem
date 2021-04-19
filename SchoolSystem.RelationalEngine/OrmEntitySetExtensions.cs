using System;
using System.Linq.Expressions;

namespace SchoolSystem.RelationalEngine
{
    public static class OrmEntitySetExtensions
    {
        public static OrmEntitySet<TEntity> Where<TEntity>(
            this OrmEntitySet<TEntity> @this,
            Expression<Func<TEntity, bool>> expression) 
            where TEntity : class
        {
            if (@this.ConditionSql == null) 
                @this.ConditionSql = $"WHERE {SqlExpressionTranslationEngine.TranslateExpression(expression)} ";
            
            else 
                @this.ConditionSql += $"AND {SqlExpressionTranslationEngine.TranslateExpression(expression)} ";

            return @this;
        }
        
        public static OrmEntitySet<TEntity> Join<TEntity>(
            this OrmEntitySet<TEntity> @this,
            string primaryKeyColumn,
            string foreignKeyColumn,
            Type entityTypeFrom,
            Type entityTypeTo) 
            where TEntity : class
        {
            @this.EntityJoinRequests.Add(new EntityJoinRequest(entityTypeFrom, entityTypeTo, primaryKeyColumn, foreignKeyColumn));

            return @this;
        }
        
        
    }
}