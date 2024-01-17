namespace OrganizationData.Application.Abstractions.DailyStats
{
    public class CsvStatsData
    {
        public int ReadRowsCount { get; set; }
        public int SavedCountriesCount { get; set; }
        public int SavedIndustriesCount { get; set; }
        public int SavedOrgsCount { get; set; }
    }
}
