using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Abstractions.FileData.DataInsertion
{
    public class BulkCollectionWrapper
    {
        public BulkCollectionWrapper()
        {
            OrganizationBulkInserts = new List<Organization>();
            IndustriesBulkInserts = new List<Industry>();
            CountriesBulkInserts = new List<Country>();
            IndOrgBulkInserts = new List<IndustryOrganization>();
        }

        public ICollection<Organization> OrganizationBulkInserts { get; set; }
        public ICollection<Industry> IndustriesBulkInserts { get; set; }
        public ICollection<Country> CountriesBulkInserts { get; set; }
        public ICollection<IndustryOrganization> IndOrgBulkInserts { get; set; }
    }
}
