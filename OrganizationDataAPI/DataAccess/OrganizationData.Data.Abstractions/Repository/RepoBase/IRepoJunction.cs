namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoJunction<TJunctionEntity> where TJunctionEntity : class 
    {
        void AddJunctionEntity(TJunctionEntity entity);
        void RemoveJunctionEntity(TJunctionEntity entity);
    }
}
