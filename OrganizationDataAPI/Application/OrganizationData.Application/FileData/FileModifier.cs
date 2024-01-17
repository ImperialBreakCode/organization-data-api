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

        private void MoveFile(string path, string newDir)
        {
            string newFilename = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.csv";
            string newPath = Path.Combine(newDir, newFilename);
            File.Move(path, newPath);
        }
    }
}
