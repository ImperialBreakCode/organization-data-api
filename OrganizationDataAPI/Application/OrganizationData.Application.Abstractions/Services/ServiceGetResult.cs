namespace OrganizationData.Application.Abstractions.Services
{
    public class ServiceGetResult<T>
    {
        public T? Result { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
