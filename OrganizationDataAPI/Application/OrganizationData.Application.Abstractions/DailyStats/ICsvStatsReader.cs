namespace OrganizationData.Application.Abstractions.DailyStats
{
    public interface ICsvStatsReader
    {
        CsvStatsData? GetCsvStatsData(DateOnly date);
    }
}
