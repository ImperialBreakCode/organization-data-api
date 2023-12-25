﻿using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Repositories
{
    internal class SqlRepositoryFactory : IRepositoryFactory
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlTransaction _sqlTransaction;

        public SqlRepositoryFactory(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = sqlTransaction;
        }

        public IRepository<T> CreateGenericRepository<T>() where T : class, IEntity
        {
            return new Repository<T>(_sqlConnection, _sqlTransaction);
        }

        public IOrganizationRepository CreateOrganizationRepository()
        {
            return new OrganizationRepository(_sqlConnection, _sqlTransaction);
        }

        IRepositoryWithJunction<T, TJunction> IRepositoryFactory.CreateGenericRepositoryWithJunction<T, TJunction>()
        {
            return new RepositoryWithJunction<T, TJunction>(_sqlConnection, _sqlTransaction);
        }
    }
}
