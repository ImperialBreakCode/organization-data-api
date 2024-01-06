namespace OrganizationData.Application.Abstractions.FileData
{
    public interface IFileReader
    {
        ICollection<OrganizationCsvData> ReadFile(string path);
    }
}
