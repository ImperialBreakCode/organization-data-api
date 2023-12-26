namespace OrganizationData.Application.ResponseMessage
{
    public static class ResponseMessages
    {
        public const string DataNotFound = "The required data is not found.";

        // Country
        public const string CountryUpdated = "The country is updated successfully.";
        public const string CountryDeleted = "The country is deleted successfully.";
        public const string CountryCreated = "The country is created successfully.";
        public const string CountryWithNameConflict = "Country with such name already exists";
    }
}
