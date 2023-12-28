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
                    return ResponseType.Conflict;

                case ServiceMessages.CountryCreated:
                case ServiceMessages.IndustryCreated:
                case ServiceMessages.OrganizationCreated:
                    return ResponseType.Created;

                default:
                    return ResponseType.Success;
            }
        }
    }
}
