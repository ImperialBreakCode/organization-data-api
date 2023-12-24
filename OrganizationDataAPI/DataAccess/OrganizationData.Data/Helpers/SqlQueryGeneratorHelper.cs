﻿namespace OrganizationData.Data.Helpers
{
    public static class SqlQueryGeneratorHelper
    {
        public static string GenerateInsertQuery<T>()
        {
            var properties = typeof(T).GetProperties();
            var propertiesList = properties.Select(p => p.Name);
            var paramsList = properties.Select(p => "@" + p.Name);

            return $"INSERT INTO [{typeof(T).Name}]({string.Join(", ", propertiesList)}) VALUES ({string.Join(", ", paramsList)})";
        }

        public static string GenerateUpdateQuery<T>()
        {
            var properties = typeof(T).GetProperties();
            var updateList = properties.Select(p => $"{p.Name} = @{p.Name}");

            return $"UPDATE [{typeof(T).Name}] SET {string.Join(", ", updateList)} WHERE id=@id";
        }

        public static string GenerateFullDeleteQuery<T>()
        {
            var properties = typeof(T).GetProperties();
            var whereList = properties.Select(p => $"{p.Name} = @{p.Name}");

            return $"DELETE FROM [{typeof(T).Name}] WHERE {string.Join(", ", whereList)}";
        }
    }
}
