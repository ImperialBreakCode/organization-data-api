using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services.Organization
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

        public void AddOrganizationIndustry(string id, string industryName)
        {
            Industry? industry = _context.Industry.GetNonDeletedIndustryByName(industryName);
            var filterResult = _dataFilter.CheckSingle(industry);
            if (!filterResult.Success)
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
        }

        public ICollection<Industry> GetOrganizationIndusties(string id)
        {
            var children = _context.Organization.GetChildrenFromJunction(id);

            ICollection<Industry> industries = children.Select(junction => _context.Industry.GetById(junction.IndustryId)).ToList();
            _dataFilter.FilterData(industries);

            return industries;
        }

        public string? RemoveOrganizationIndustry(string id, string industryId)
        {
            Industry? industry = _context.Industry.GetNonDeletedIndustryByName(industryId);
            var filterResult = _dataFilter.CheckSingle(industry);
            if (!filterResult.Success)
            {
                return ResponseMessages.DataNotFound;
            }

            _context.Organization.RemoveJunctionEntity(new()
            {
                IndustryId = industryId,
                OrganizationId = id
            });

            return null;
        }
    }
}
