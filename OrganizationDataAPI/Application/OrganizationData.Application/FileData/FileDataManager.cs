using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.FileData
{
    internal class FileDataManager : IFileDataManager
    {
        private readonly IOrganizationSettings _organizationSettings;
        private readonly IFileReader _reader;
        private readonly IFileModifier _modifier;
        private readonly IFileDataInserter _inserter;

        public FileDataManager(IOrganizationSettings organizationSettings, IFileReader reader, IFileModifier modifier, IFileDataInserter inserter)
        {
            _organizationSettings = organizationSettings;
            _reader = reader;
            _modifier = modifier;
            _inserter = inserter;
        }

        public void SaveDataFromFiles()
        {
            string[] files = Directory.GetFiles(_organizationSettings.FileReaderDir);

            for (int i = 0; i < files.Length; i++)
            {
                var data = _reader.ReadFile(files[i]);

                foreach (var d in data)
                {
                    _inserter.SaveData(d);
                }

                _modifier.MarkFileAsRead(files[i]);
            }
        }
    }
}
