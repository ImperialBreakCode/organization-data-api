using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Application.Abstractions.Services.Filter
{
    public interface IDataFilter
    {
        FilterResult CheckSingle(IEntity? entity, bool includeSoftDeleted = false);
        ICollection<T> FilterData<T>(ICollection<T> entities, bool includeSoftDeleted = false) where T : class, IEntity;
    }
}
