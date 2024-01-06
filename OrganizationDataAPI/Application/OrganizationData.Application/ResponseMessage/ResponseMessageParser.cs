namespace OrganizationData.Application.ResponseMessage
{
    public static class ResponseMessageParser
    {
        public static ResponseType Parse(string response)
        {
            switch (response)
            {
                case ServiceMessages.DataNotFound:
                    return ResponseType.NotFound;

                case ServiceMessages.CountryNameConflict:
                case ServiceMessages.IndustryNameConflict:
                case ServiceMessages.OrganizationIndusryAlreadyExists:
                case ServiceMessages.OrganizationIdConflict:
                case ServiceMessages.UsernameConflict:
                    return ResponseType.Conflict;

                case ServiceMessages.CountryCreated:
                case ServiceMessages.IndustryCreated:
                case ServiceMessages.OrganizationCreated:
                case ServiceMessages.UserCreated:
                    return ResponseType.Created;

                case ServiceMessages.LoginIncorrectCredentials:
                    return ResponseType.Unauthorized;

                default:
                    return ResponseType.Success;
            }
        }
    }
}
