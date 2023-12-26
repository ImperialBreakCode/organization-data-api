namespace OrganizationData.Application.ResponseMessage
{
    public static class ResponseMessageParser
    {
        public static ResponseType Parse(string response)
        {
            switch (response)
            {
                case ResponseMessages.DataNotFound:
                    return ResponseType.NotFound;

                case ResponseMessages.CountryWithNameConflict:
                    return ResponseType.Conflict;

                case ResponseMessages.CountryCreated:
                    return ResponseType.Created;

                default:
                    return ResponseType.Success;
            }
        }
    }
}
