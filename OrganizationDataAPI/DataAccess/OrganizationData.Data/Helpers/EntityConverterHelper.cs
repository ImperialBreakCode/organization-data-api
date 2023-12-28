using Microsoft.Data.SqlClient;

namespace OrganizationData.Data.Helpers
{
    public static class EntityConverterHelper
    {
        public static ICollection<T> ToEntityCollection<T>(SqlCommand command) where T : class
        {
            var collection = new List<T>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T entity = (T)Activator.CreateInstance(typeof(T))!;
                    ToEntity(entity, reader);
                    collection.Add(entity);
                }
            }

            return collection;
        }

        public static T ToEntity<T>(T entity, SqlDataReader reader) where T : class
        {
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = reader[property.Name];

                if (value == DBNull.Value)
                {
                    value = null;
                }

                property.SetValue(entity, value);
            }

            return entity;
        }

        public static void ToQuery<T>(T entity, SqlCommand command) where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);

                value ??= DBNull.Value;

                command.Parameters.AddWithValue($"@{property.Name}", value);
            }
        }
    }
}
