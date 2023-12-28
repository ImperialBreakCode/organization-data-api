using Microsoft.Data.SqlClient;

namespace OrganizationData.Data.Abstractions.DbManager
{
    public interface IOrganizationTableExistenceChecker
    {
        SqlConnection SqlConnection { get; set; }
        bool CheckIfTableExists<T>() where T : class;
    }
}
