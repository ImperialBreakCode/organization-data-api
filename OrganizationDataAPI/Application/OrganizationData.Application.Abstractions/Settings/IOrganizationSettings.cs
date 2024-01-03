namespace OrganizationData.Application.Abstractions.Settings
{
    public interface IOrganizationSettings
    {
        string ConnectionString { get; }
        string FileReaderDir { get; }
        string ProcessedFilesDir { get; }
    }
}
