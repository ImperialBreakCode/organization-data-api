using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services.OrganizationServices
{
    internal class OrganizationDataHelper : IOrganizationDataHelper
    {
        private IOrganizationDbContext _context;
        private IDataFilter _dataFilter;

        public OrganizationDataHelper(IDataFilter dataFilter)
        {
            _dataFilter = dataFilter;
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
                industry = new Industry()
                {
                    IndustryName = industryName
                };

                _context.Industry.Insert(industry);
            }

            _context.Organization.AddJunctionEntity(new()
            {
                OrganizationId = id,
                IndustryId = industry!.Id
            });

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

            _context.Organization.RemoveJunctionEntity(new()
            {
                IndustryId = industry!.Id,
                OrganizationId = id
            });

            return null;
        }
    }
}
