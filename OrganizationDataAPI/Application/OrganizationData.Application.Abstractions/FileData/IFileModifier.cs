namespace OrganizationData.Application.Abstractions.FileData
{
    public interface IFileModifier
    {
        void MarkFileAsRead(string path);
        void MarkFileAsFailed(string path);
    }
}
