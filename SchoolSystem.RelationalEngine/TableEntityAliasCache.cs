using System;
using System.Collections.Generic;

namespace SchoolSystem.RelationalEngine
{
    public static class TableEntityAliasCache
    {
        private static readonly Dictionary<Type, string> AliasCache = new Dictionary<Type, string>();
        private static int _aliasCounter = 1;
        private static readonly string _aliasPrefix = "e";

        public static string GetOrAddAlias(Type entityType)
        {
            var existingAlias = AliasCache.GetValueOrDefault(entityType);
            if (existingAlias != null)
                return existingAlias;
            
            var generatedAlias = _aliasPrefix + "_" +  _aliasCounter++;
            
            AliasCache.Add(entityType, generatedAlias);

            return generatedAlias;
        }
    }
}