using OrganizationData.Application.Abstractions.DailyStats;
using OrganizationData.Application.Abstractions.Services;

namespace OrganizationData.Application.Services
{
    internal class DailyCsvStatsService : IDailyCsvStatsService
    {
        private readonly IPDFGenerator _pdfGenerator;
        private readonly ICsvStatsReader _csvStatsReader;

        public DailyCsvStatsService(IPDFGenerator pdfGenerator, ICsvStatsReader csvStatsReader)
        {
            _pdfGenerator = pdfGenerator;
            _csvStatsReader = csvStatsReader;
        }

        public byte[]? GetStatsForADate(int day, int month, int year, int daysAhead)
        {
            DateTime date = new(year, month, day);

            if (date > DateTime.UtcNow)
            {
                return null;
            }

            Dictionary<DateTime, CsvStatsData> csvDatas = new Dictionary<DateTime, CsvStatsData>();

            for (int i = 0; i < daysAhead; i++)
            {
                var newDate = date.AddDays(i);

                if (newDate > DateTime.UtcNow)
                {
                    break;
                }

                var csvData = _csvStatsReader.GetCsvStatsData(DateOnly.FromDateTime(newDate));
                if (csvData is not null)
                {
                    csvDatas.Add(newDate, csvData);
                }
            }

            return _pdfGenerator.GeneratePdfFromStats(csvDatas);
        }
    }
}
