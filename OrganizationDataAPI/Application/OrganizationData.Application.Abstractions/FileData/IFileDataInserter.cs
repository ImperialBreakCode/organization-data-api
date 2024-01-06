namespace OrganizationData.Application.Abstractions.FileData
{
    public interface IFileDataInserter
    {
        void SaveData(OrganizationCsvData data);
    }
}
