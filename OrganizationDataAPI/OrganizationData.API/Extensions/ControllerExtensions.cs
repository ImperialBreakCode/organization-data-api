using Microsoft.AspNetCore.Mvc;
using OrganizationData.Application.ResponseMessage;

namespace OrganizationData.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ParseAndReturnMessage(this ControllerBase controller, string message)
        {
            ResponseType responseType = ResponseMessageParser.Parse(message);

            switch (responseType)
            {
                case ResponseType.NotFound:
                    return controller.NotFound(message);
                case ResponseType.Conflict:
                    return controller.Conflict(message);
                case ResponseType.Created:
                    return controller.StatusCode(201, message);
                case ResponseType.Unauthorized:
                    return controller.Unauthorized(message);
                default:
                    return controller.Ok(message);
            }
        }
    }
}
