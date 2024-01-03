using CsvHelper;
using OrganizationData.Application.Abstractions.FileData;
using System.Globalization;

namespace OrganizationData.Application.FileData
{
    internal class FileReader : IFileReader
    {
        public ICollection<OrganizationCsvData> ReadFile(string path)
        {
            ICollection<OrganizationCsvData> result;

            using (var reader = new StreamReader(path))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                result = csvReader.GetRecords<OrganizationCsvData>().ToList();
            }

            return result;
        }
    }
}
