using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace SchoolSystem.RelationalEngine
{
    public static class SqlExpressionTranslationEngine
    {
        private static string CreateStringOperator(ExpressionType expressionType)
        {
            return expressionType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "!=",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                _ => throw new NotImplementedException("No other expression type is expected."),
            };
        }

        public static string TranslateExpression(Expression expression)
        {
            if (!(expression is LambdaExpression))
                throw new SystemException("The expression can be only the lambda.");

            var lambdaExpression = (LambdaExpression)expression;

            if (!(lambdaExpression.Body is BinaryExpression))
                throw new NotImplementedException("The other expressions are not implemented.");

            var expressionBody = (BinaryExpression)lambdaExpression.Body;

            return InternalTranslateSide(expressionBody.Left, "left") + " " +
                   CreateStringOperator(expressionBody.NodeType) + " " +
                   InternalTranslateSide(expressionBody.Right, "right");
        }

        private static string InternalTranslateSide(Expression expressionSide, string side)
        {
            if (expressionSide is MethodCallExpression)
                throw new SystemException("The expression could not be translated. The method call is forbidden.");


            // If the current expression is binary expression we need to do recursive call to get to the most basic expression
            if (expressionSide is BinaryExpression binaryExpression)
            {
                return InternalTranslateSide(binaryExpression.Left, "left") + " " +
                       CreateStringOperator(binaryExpression.NodeType) + " " +
                       InternalTranslateSide(binaryExpression.Right, "right");
            }



            // We expect it to be property at the beginning
            if (!(expressionSide is MemberExpression || expressionSide is ConstantExpression))
                throw new Exception("The first branch of the expression has to be property.");

            switch (expressionSide)
            {
                case MemberExpression propertyExpression:
                    {
                        var propertyColumnAttribute = propertyExpression.Member.GetCustomAttribute(typeof(ColumnAttribute));
                        string propertyName = "";
                        if (side == "left")
                        {
                            propertyName = TableEntityAliasCache.GetOrAddAlias(propertyExpression.Expression.Type);
                            propertyName += "." + (propertyColumnAttribute == null
                                ? propertyExpression.Member.Name
                                : (propertyColumnAttribute as ColumnAttribute)?.Name);
                        }
                        else if (side == "right")
                        {
                            var ret = GetValue(propertyExpression);

                            if (ret is string)
                                return $"'{ret}'";

                            if (ret is DateTime time)
                                return $"'{time:d}'";

                            if (ret is int || ret is double || ret is float)
                                return $"{ret}";
                        }
                        else
                        {
                            throw new Exception();
                        }
                        return propertyName;
                    }

                case ConstantExpression constantExpression:
                    {
                        return constantExpression.Type != typeof(int) ?
                            $"'{constantExpression.Value}'"
                            : constantExpression.ToString();
                    }

                default:
                    return null;
            }
        }
        
        private static object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }
    }
}