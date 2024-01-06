using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Factories;

namespace OrganizationData.Application.Services.Factories
{
    internal class ServiceGetResultFactory : IServiceGetResultFactory
    {
        public ServiceGetResult<T> CreateGetServiceResult<T>(T? dto, string? errorMessage)
        {
            return new ServiceGetResult<T>()
            {
                Result = dto,
                ErrorMessage = errorMessage
            };
        }
    }
}
