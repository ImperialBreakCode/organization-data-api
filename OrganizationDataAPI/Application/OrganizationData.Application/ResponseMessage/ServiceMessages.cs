namespace OrganizationData.Application.ResponseMessage
{
    public static class ServiceMessages
    {
        public const string DataNotFound = "The required data is not found.";

        // User
        public const string UserCreated = "Account created.";
        public const string UserDeleted = "Account deleted successfully.";
        public const string UsernameConflict = "Account with the same username already exists.";
        public const string LoginSuccessfull = "Login successfull.";
        public const string LoginIncorrectCredentials = "Incorrect credentials.";

        // Country
        public const string CountryUpdated = "The country is updated successfully.";
        public const string CountryDeleted = "The country is deleted successfully.";
        public const string CountryCreated = "The country is created successfully.";
        public const string CountryNameConflict = "Country with such name already exists.";

        // Industry
        public const string IndustryNameConflict = "Industry with such name already exists.";
        public const string IndustryCreated = "The industry is created successfully.";
        public const string IndustryUpdated = "The industry is updated successfully.";
        public const string IndustryDeleted = "The industry is deleted successfully.";

        //Organization
        public const string OrganizationIdConflict = "Organization with such id already exists.";
        public const string OrganizationIndusryAlreadyExists = "The organization is already part of that industry.";
        public const string OrganizationIndusryAdded = "Industry is successfully added to the organization.";
        public const string OrganizationIndusryRemoved = "Industry is successfully removed from the organization.";
        public const string OrganizationCreated = "The organization is created successfully.";
        public const string OrganizationUpdated = "The organization is updated successfully.";
        public const string OrganizationDeleted = "The organization is deleted successfully.";
    }
}
