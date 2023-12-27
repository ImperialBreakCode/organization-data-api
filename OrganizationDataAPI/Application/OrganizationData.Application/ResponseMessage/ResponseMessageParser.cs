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

                case ResponseMessages.CountryNameConflict:
                case ResponseMessages.IndustryNameConflict:
                    return ResponseType.Conflict;

                case ResponseMessages.CountryCreated:
                case ResponseMessages.IndustryCreated:
                    return ResponseType.Created;

                default:
                    return ResponseType.Success;
            }
        }
    }
}
