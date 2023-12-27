using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Base;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    internal class RepositoryWithJunction<T, TJUnctionEntity> : Repository<T>, IRepositoryWithJunction<T, TJUnctionEntity>
        where T : class, IEntity
        where TJUnctionEntity : class
    {
        private readonly string _junctionInsertQuery;
        private readonly string _junctionDeleteQuery;

        public RepositoryWithJunction(ISqlConnectionWrapper sqlConnectionWrapper) 
            : base(sqlConnectionWrapper)
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
