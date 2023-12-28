using Microsoft.Data.SqlClient;

namespace OrganizationData.Data.Abstractions.DbManager
{
    public interface IOrganizationTableCreator
    {
        SqlConnection SqlConnection { get; set; }
        void CreateTable<T>() where T : class;
    }
}
