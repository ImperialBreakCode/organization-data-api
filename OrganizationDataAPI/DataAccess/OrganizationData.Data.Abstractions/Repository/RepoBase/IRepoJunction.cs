namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoJunction<TJunctionEntity> where TJunctionEntity : class 
    {
        void ConnectToJunctionEntity(TJunctionEntity entity);
        void DisconnectFromJunctionEntity(TJunctionEntity entity);
    }
}
