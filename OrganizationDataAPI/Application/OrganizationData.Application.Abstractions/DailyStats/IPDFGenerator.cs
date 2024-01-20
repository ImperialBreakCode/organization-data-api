namespace OrganizationData.Application.Abstractions.DailyStats
{
    public interface IPDFGenerator
    {
        byte[] GeneratePdfFromStats(Dictionary<DateTime, CsvStatsData> datas);
    }
}
