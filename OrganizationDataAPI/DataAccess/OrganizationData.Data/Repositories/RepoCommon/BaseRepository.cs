using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbConnectionWrapper;

namespace OrganizationData.Data.Repositories.RepoCommon
{
    internal abstract class BaseRepository
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        public BaseRepository(ISqlConnectionWrapper sqlConnectionWrapper)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
        }

        protected SqlCommand CreateCommand(string query)
        {
            SqlCommand sqlCommand = _sqlConnectionWrapper.SqlConnection.CreateCommand();
            sqlCommand.CommandText = query;
            sqlCommand.Transaction = _sqlConnectionWrapper.SqlTransaction;

            return sqlCommand;
        }
    }
}
