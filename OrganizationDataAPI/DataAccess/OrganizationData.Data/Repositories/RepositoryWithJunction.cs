using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Base;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    public class RepositoryWithJunction<T, TJUnctionEntity> : Repository<T>, IRepositoryWithJunction<T, TJUnctionEntity>
        where T : class, IEntity
        where TJUnctionEntity : class
    {
        private readonly string _junctionInsertQuery;
        private readonly string _junctionDeleteQuery;

        public RepositoryWithJunction(SqlConnection sqlConnection, SqlTransaction sqlTransaction) 
            : base(sqlConnection, sqlTransaction)
        {
            _junctionInsertQuery = SqlQueryGeneratorHelper.GenerateInsertQuery<TJUnctionEntity>();
            _junctionDeleteQuery = SqlQueryGeneratorHelper.GenerateFullDeleteQuery<TJUnctionEntity>();
        }

        public void ConnectToJunctionEntity(TJUnctionEntity entity)
        {
            var command = CreateCommand(_junctionInsertQuery);
            EntityConverterHelper.ToQuery(entity, command);
            command.ExecuteNonQuery();
        }

        public void DisconnectFromJunctionEntity(TJUnctionEntity entity)
        {
            var command = CreateCommand(_junctionDeleteQuery);
            EntityConverterHelper.ToQuery(entity, command);
            command.ExecuteNonQuery();
        }
    }
}
