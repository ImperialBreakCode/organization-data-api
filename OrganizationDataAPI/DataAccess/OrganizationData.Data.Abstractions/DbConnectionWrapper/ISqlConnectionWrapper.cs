using Microsoft.Data.SqlClient;

namespace OrganizationData.Data.Abstractions.DbConnectionWrapper
{
    public interface ISqlConnectionWrapper
    {
        SqlConnection SqlConnection { get; }
        SqlTransaction SqlTransaction { get; }

        void ConnectToDb(string connectionString);
        void BeginNewTransaction();
    }
}
