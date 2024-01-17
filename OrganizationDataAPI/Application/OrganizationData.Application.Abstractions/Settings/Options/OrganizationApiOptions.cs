using OrganizationData.Application.Abstractions.Settings.Options.OrganizationApi;

namespace OrganizationData.Application.Abstractions.Settings.Options
{
    public class OrganizationApiOptions
    {
        public string FileReaderDir { get; set; }
        public string ProcessedFilesDir { get; set; }
        public string FailedFilesDir { get; set; }
        public AuthSettings AuthSettings { get; set; }
        public string CsvStatsDir { get; set; }
    }
}
