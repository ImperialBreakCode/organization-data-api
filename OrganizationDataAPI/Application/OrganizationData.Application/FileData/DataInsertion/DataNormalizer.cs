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
        private readonly IOrganizationIdsSet _organizationIdsSet;

        private Dictionary<string, string> _avaliableIndustries;
        private Dictionary<string, string> _avaliableCountries;

        public DataNormalizer
            (IOrganizationData organizationData, 
            IEntityFactory entityFactory, 
            IMapper mapper, 
            IOrganizationIdsSet organizationIdsSet)
        {
            _organizationDbContext = organizationData.DbContext;
            _entityFactory = entityFactory;
            _mapper = mapper;
            _organizationIdsSet = organizationIdsSet;
        }

        public BulkCollectionWrapper NormalizeData(ICollection<OrganizationCsvData> data)
        {
            LoadCurrentData();

            var bulkCollection = new BulkCollectionWrapper();
            foreach (var item in data)
            {
                ProcessOrganization(item, bulkCollection);
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

        private void ProcessOrganization(OrganizationCsvData item, BulkCollectionWrapper bulkCollection)
        {
            if (_organizationIdsSet.ContainsId(item.OrganizationId))
            {
                return;
            }
            
            _organizationIdsSet.AddId(item.OrganizationId);

            var organizationBulk = _mapper.Map<Organization>(item);

            organizationBulk.CountryId = ProcessCountry(item.Country, bulkCollection.CountriesBulkInserts);

            var industries = SplitIndustries(item.Industry);
            var industryIds = ProcessIndustries(industries, bulkCollection.IndustriesBulkInserts);

            foreach (var industryId in industryIds)
            {
                bulkCollection.IndOrgBulkInserts.Add(_entityFactory.CreateIndustryOrganizationJunction(organizationBulk.Id, industryId));
            }

            bulkCollection.OrganizationBulkInserts.Add(organizationBulk);
        }

        private string ProcessCountry(string countryName, ICollection<Country> bulkCountries)
        {
            string countryId;
            if (!_avaliableCountries.ContainsKey(countryName))
            {
                var country = _entityFactory.CreateCountryEntity(countryName);
                bulkCountries.Add(country);
                _avaliableCountries.Add(country.CountryName, country.Id);

                countryId = country.Id;
            }
            else
            {
                countryId = _avaliableCountries[countryName];
            }

            return countryId;
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
