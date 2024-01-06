using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository.RepoCommon;
using OrganizationData.Data.Entities.Base;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories.RepoCommon
{
    internal class Repository<T> : BaseRepository, IRepository<T> where T : class, IEntity
    {
        private readonly string _insertQuery;
        private readonly string _updateQuery;
        private readonly string _getByIdQuery;
        private readonly string _getAllQuery;

        public Repository(ISqlConnectionWrapper sqlConnectionWrapper)
            : base(sqlConnectionWrapper)
        {
            _insertQuery = SqlQueryGeneratorHelper.GenerateInsertQuery<T>();
            _updateQuery = SqlQueryGeneratorHelper.GenerateUpdateQuery<T>();
            _getByIdQuery = $"SELECT * FROM [{typeof(T).Name}] WHERE Id=@id";
            _getAllQuery = $"SELECT * FROM [{typeof(T).Name}]";
        }

        protected string InsertQuery => _insertQuery;
        protected string UpdateQuery => _updateQuery;
        protected string GetByIdQuery => _getByIdQuery;
        protected string GetAllQuery => _getAllQuery;

        public T? GetById(string id)
        {
            var command = CreateCommand(GetByIdQuery);
            command.Parameters.AddWithValue("@id", id);

            return EntityConverterHelper.ToEntityCollection<T>(command).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            var command = CreateCommand(InsertQuery);

            EntityConverterHelper.ToQuery(entity, command);

            command.ExecuteNonQuery();
        }

        public virtual void SoftDelete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            Update(entity);
        }

        public void Update(T entity)
        {
            var command = CreateCommand(UpdateQuery);

            EntityConverterHelper.ToQuery(entity, command);

            command.ExecuteNonQuery();
        }
    }
}
