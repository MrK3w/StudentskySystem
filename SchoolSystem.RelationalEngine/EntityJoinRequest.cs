using System;

namespace SchoolSystem.RelationalEngine
{
    internal class EntityJoinRequest
    {
        public EntityJoinRequest(Type entityTypeFrom,Type entityTypeTo, string primaryKeyColumn, string foreignKeyColumn)
        {
            EntityTypeFrom = entityTypeFrom;
            EntityTypeTo = entityTypeTo;
            PrimaryKeyColumn = primaryKeyColumn;
            ForeignKeyColumn = foreignKeyColumn;
        }

        public Type EntityTypeFrom
        {
            get;
        }

        public Type EntityTypeTo
        {
            get;
        }

        public string PrimaryKeyColumn
        {
            get;
        }
        
        public string ForeignKeyColumn
        {
            get;
        }
    }
}