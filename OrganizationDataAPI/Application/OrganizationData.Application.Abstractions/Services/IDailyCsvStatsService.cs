namespace OrganizationData.Application.Abstractions.Services
{
    public interface IDailyCsvStatsService
    {
        public byte[]? GetStatsForADate(int day, int month, int year, int daysAhead);
    }
}
