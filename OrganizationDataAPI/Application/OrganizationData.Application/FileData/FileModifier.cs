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

        public void MarkFileAsFailed(string path)
        {
            MoveFile(path, _settings.FailedFilesDir);
        }

        public void MarkFileAsRead(string path)
        {
            MoveFile(path, _settings.ProcessedFilesDir);
        }

        private void MoveFile(string path, string newFolder)
        {
            string newFilename = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            File.Move(path, newFolder + $"/{newFilename}.csv");
        }
    }
}
