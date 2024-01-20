using OrganizationData.Application.Abstractions.DailyStats;

namespace OrganizationData.Application.DailyStats
{
    internal class CsvStatsFactory : ICsvDataFactory
    {
        public CsvStatsData CreateDailyStatsData()
        {
            return new CsvStatsData();
        }
    }
}
