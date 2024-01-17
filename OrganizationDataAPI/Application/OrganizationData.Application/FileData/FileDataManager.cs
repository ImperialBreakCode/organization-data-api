using CsvHelper;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.FileData
{
    internal class FileDataManager : IFileDataManager
    {
        private readonly IOrganizationSettings _organizationSettings;
        private readonly IFileReader _reader;
        private readonly IFileModifier _modifier;
        private readonly IFileDataInserter _inserter;
        private readonly IDataNormalizer _dataNormalizer;

        public FileDataManager(IOrganizationSettings organizationSettings, IFileReader reader, IFileModifier modifier, IFileDataInserter inserter, IDataNormalizer dataNormalizer)
        {
            _organizationSettings = organizationSettings;
            _reader = reader;
            _modifier = modifier;
            _inserter = inserter;
            _dataNormalizer = dataNormalizer;
        }

        public void SaveDataFromFiles()
        {
            string[] files = Directory.GetFiles(_organizationSettings.FileReaderDir);

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    var data = _reader.ReadFile(files[i]);
                    var collectionWrapper = _dataNormalizer.NormalizeData(data);
                    _inserter.SaveData(collectionWrapper);

                    _modifier.MarkFileAsRead(files[i]);
                }
                catch (CsvHelperException)
                {
                    _modifier.MarkFileAsFailed(files[i]);
                }
                
            }
        }
    }
}
