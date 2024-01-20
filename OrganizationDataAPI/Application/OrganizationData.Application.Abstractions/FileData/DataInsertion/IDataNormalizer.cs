namespace OrganizationData.Application.Abstractions.FileData.DataInsertion
{
    public interface IDataNormalizer
    {
        BulkCollectionWrapper NormalizeData(ICollection<OrganizationCsvData> data);
    }
}
