using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.FileData
{
    internal class FileModifier : IFileModifier
    {
        private readonly IOrganizationSettings _settings;

        public FileModifier(IOrganizationSettings settings)
        {
            _settings = settings;
        }

        public void MarkFileAsRead(string path)
        {
            string newFilename = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            File.Move(path, _settings.ProcessedFilesDir + $"/{newFilename}.txt");
        }
    }
}
