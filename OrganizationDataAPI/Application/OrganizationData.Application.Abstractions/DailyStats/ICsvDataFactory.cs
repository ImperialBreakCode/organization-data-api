namespace OrganizationData.Application.Abstractions.DailyStats
{
    public interface ICsvDataFactory
    {
        CsvStatsData CreateDailyStatsData();
    }
}
