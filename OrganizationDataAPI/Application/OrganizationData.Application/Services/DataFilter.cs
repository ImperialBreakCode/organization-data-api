using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Application.Services
{
    internal class DataFilter : IDataFilter
    {
        public FilterResult CheckSingle(IEntity? entity, bool includeSoftDeleted = false)
        {
            if (entity is null || !includeSoftDeleted && entity.DeletedAt is not null)
            {
                return new FilterResult()
                {
                    Success = false,
                    ErrorMessage = ResponseMessages.DataNotFound
                };
            }

            return new FilterResult()
            {
                Success = true,
            };
        }

        public ICollection<T> FilterData<T>(ICollection<T> entities, bool includeSoftDeleted = false)
            where T : class, IEntity
        {
            return entities.Where(e => !(!includeSoftDeleted && e.DeletedAt is not null)).ToList();
        }
    }
}
