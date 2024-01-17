using Newtonsoft.Json;
using OrganizationData.Application.Abstractions.DailyStats;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.DailyStats
{
    internal class CsvStatsWriter : ICsvStatsWriter
    {
        private readonly IOrganizationSettings _organizationSettings;

        public CsvStatsWriter(IOrganizationSettings organizationSettings)
        {
            _organizationSettings = organizationSettings;
        }

        public void SaveData(CsvStatsData data)
        {
            DateTime now = DateTime.UtcNow;
            string filename = $"{now.Day}-{now.Month}-{now.Year}.json";
            string path = Path.Combine(_organizationSettings.CsvStatsDir, filename);

            CsvStatsData csvStatsData;
            if (File.Exists(path))
            {
                csvStatsData = JsonConvert.DeserializeObject<CsvStatsData>(File.ReadAllText(path));
            }
            else
            {
                csvStatsData = new CsvStatsData();
            }

            csvStatsData!.SavedIndustriesCount += data.SavedIndustriesCount;
            csvStatsData.SavedCountriesCount += data.SavedCountriesCount;
            csvStatsData.SavedOrgsCount += data.SavedOrgsCount;
            csvStatsData.ReadRowsCount += data.ReadRowsCount;

            File.WriteAllText(path, JsonConvert.SerializeObject(csvStatsData));
        }
    }
}
