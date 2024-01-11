using AutoMapper;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.FileData.DataInsertion
{
    internal class DataNormalizer : IDataNormalizer
    {
        private readonly IOrganizationDbContext _organizationDbContext;
        private readonly IEntityFactory _entityFactory;
        private readonly IMapper _mapper;

        private Dictionary<string, string> _avaliableIndustries;
        private Dictionary<string, string> _avaliableCountries;

        public DataNormalizer(IOrganizationData organizationData, IEntityFactory entityFactory, IMapper mapper)
        {
            _organizationDbContext = organizationData.DbContext;
            _entityFactory = entityFactory;
            _mapper = mapper;
        }

        public BulkCollectionWrapper NormalizeData(ICollection<OrganizationCsvData> data)
        {
            LoadCurrentData();

            var bulkCollection = new BulkCollectionWrapper();

            foreach (var item in data)
            {
                if (_organizationDbContext.Organization.CheckIfExistsByOrganizationId(item.OrganizationId))
                {
                    continue;
                }

                var organizationBulk = _mapper.Map<Organization>(item);

                string countryId;
                if (!_avaliableCountries.ContainsKey(item.Country))
                {
                    var country = _entityFactory.CreateCountryEntity(item.Country);
                    bulkCollection.CountriesBulkInserts.Add(country);
                    _avaliableCountries.Add(country.CountryName, country.Id);

                    countryId = country.Id;
                }
                else
                {
                    countryId = _avaliableCountries[item.Country];
                }

                organizationBulk.CountryId = countryId;

                var industries = SplitIndustries(item.Industry);
                var industryIds = ProcessIndustries(industries, bulkCollection.IndustriesBulkInserts);

                foreach (var industryId in industryIds)
                {
                    bulkCollection.IndOrgBulkInserts.Add(_entityFactory.CreateIndustryOrganizationJunction(organizationBulk.Id, industryId));
                }

                bulkCollection.OrganizationBulkInserts.Add(organizationBulk);
            }

            _avaliableCountries.Clear();
            _avaliableIndustries.Clear();

            return bulkCollection;
        }

        private void LoadCurrentData()
        {
            _avaliableCountries = _organizationDbContext.Country
                .GetAll()
                .ToDictionary(c => c.CountryName, c => c.Id);

            _avaliableIndustries = _organizationDbContext.Industry
                .GetAll()
                .ToDictionary(c => c.IndustryName, c => c.Id);
        }

        private ICollection<string> SplitIndustries(string rawIndustries)
        {
            rawIndustries = rawIndustries.Replace("\\s+", " ");

            ICollection<string> normalizedIndustries = rawIndustries.Split(" / ").ToList();

            return normalizedIndustries;
        }

        private ICollection<string> ProcessIndustries(ICollection<string> industryNames, ICollection<Industry> bulkIndustries)
        {
            var industryIds = new List<string>();

            foreach (var industry in industryNames)
            {
                if (!_avaliableIndustries.ContainsKey(industry))
                {
                    var industryEntity = _entityFactory.CreateIndustryEntity(industry);
                    bulkIndustries.Add(industryEntity);
                    _avaliableIndustries.Add(industryEntity.IndustryName, industryEntity.Id);

                    industryIds.Add(industryEntity.Id);
                }
                else
                {
                    industryIds.Add(_avaliableIndustries[industry]);
                }
            }

            return industryIds;
        }
    }
}
