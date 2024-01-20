namespace OrganizationData.Application.Abstractions.FileData.DataInsertion
{
    public interface IFileDataInserter
    {
        void SaveData(BulkCollectionWrapper bulkCollectionWrapper);
    }
}
