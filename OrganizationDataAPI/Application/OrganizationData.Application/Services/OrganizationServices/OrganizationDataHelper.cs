using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services.OrganizationServices
{
    internal class OrganizationDataHelper : IOrganizationDataHelper
    {
        private IOrganizationDbContext _context;
        private IDataFilter _dataFilter;
        private IEntityFactory _entityFactory;

        public OrganizationDataHelper(IDataFilter dataFilter, IEntityFactory entityFactory)
        {
            _dataFilter = dataFilter;
            _entityFactory = entityFactory;
        }

        public void SetOrganizationDbContext(IOrganizationDbContext context)
        {
            _context = context;
        }

        public string? AddOrganizationIndustry(string id, string industryName)
        {
            ICollection<IndustryOrganization> junctions = _context.Organization.GetChildrenFromJunction(id);

            Industry? industry = _context.Industry.GetNonDeletedIndustryByName(industryName);
            var filterResult = _dataFilter.CheckSingle(industry);

            if (filterResult.Success && junctions.Any(j => j.IndustryId == industry!.Id))
            {
                return ServiceMessages.OrganizationIndusryAlreadyExists;
            }
            else if(!filterResult.Success)
            {
                industry = _entityFactory.CreateIndustryEntity(industryName);

                _context.Industry.Insert(industry);
            }

            _context.Organization.AddJunctionEntity(_entityFactory.CreateIndustryOrganizationJunction(id, industry!.Id));

            return null;
        }

        public ICollection<Industry> GetOrganizationIndusties(string id)
        {
            var children = _context.Organization.GetChildrenFromJunction(id);

            ICollection<Industry> industries = children.Select(junction => _context.Industry.GetById(junction.IndustryId)).ToList();
            _dataFilter.FilterData(industries);

            return industries;
        }

        public string? RemoveOrganizationIndustry(string id, string industryName)
        {
            Industry? industry = _context.Industry.GetNonDeletedIndustryByName(industryName);
            var filterResult = _dataFilter.CheckSingle(industry);
            if (!filterResult.Success)
            {
                return ServiceMessages.DataNotFound;
            }

            _context.Organization
                .RemoveJunctionEntity(_entityFactory.CreateIndustryOrganizationJunction(id, industry!.Id));

            return null;
        }
    }
}
