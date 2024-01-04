namespace OrganizationData.Application.Abstractions.Services.Factories
{
    public interface IServiceGetResultFactory
    {
        ServiceGetResult<T> CreateGetServiceResult<T>(T? dto, string? errorMessage);
    }
}
