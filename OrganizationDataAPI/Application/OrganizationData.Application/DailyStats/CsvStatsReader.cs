using Newtonsoft.Json;
using OrganizationData.Application.Abstractions.DailyStats;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.DailyStats
{
    internal class CsvStatsReader : ICsvStatsReader
    {
        private readonly IOrganizationSettings _organizationSettings;

        public CsvStatsReader(IOrganizationSettings organizationSettings)
        {
            _organizationSettings = organizationSettings;
        }

        public CsvStatsData? GetCsvStatsData(DateOnly date)
        {
            var path = Path.Combine(_organizationSettings.CsvStatsDir, $"{date.Day}-{date.Month}-{date.Year}.json");

            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<CsvStatsData>(File.ReadAllText(path));
            }

            return null;
        }
    }
}
