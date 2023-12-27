namespace OrganizationData.Application.ResponseMessage
{
    public static class ResponseMessages
    {
        public const string DataNotFound = "The required data is not found.";

        // Country
        public const string CountryUpdated = "The country is updated successfully.";
        public const string CountryDeleted = "The country is deleted successfully.";
        public const string CountryCreated = "The country is created successfully.";
        public const string CountryNameConflict = "Country with such name already exists";

        // Industry
        public const string IndustryNameConflict = "Industry with such name already exists";
        public const string IndustryCreated = "The industry is created successfully.";
        public const string IndustryUpdated = "The industry is updated successfully.";
        public const string IndustryDeleted = "The industry is deleted successfully.";
    }
}
