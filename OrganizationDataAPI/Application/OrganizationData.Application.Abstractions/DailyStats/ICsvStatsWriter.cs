namespace OrganizationData.Application.Abstractions.DailyStats
{
    public interface ICsvStatsWriter
    {
        void SaveData(CsvStatsData data);
    }
}
