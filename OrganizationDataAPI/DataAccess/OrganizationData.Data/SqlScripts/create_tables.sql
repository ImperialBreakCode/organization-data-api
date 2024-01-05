CREATE TABLE [Country](
    Id varchar(36) NOT NULL PRIMARY KEY,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
    CountryName varchar(64) NOT NULL,
    UNIQUE(CountryName, DeletedAt)
);

CREATE TABLE [Organization](
    Id varchar(36) NOT NULL PRIMARY KEY,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
    OrganizationId varchar(50) NOT NULL,
    Name varchar(128) NOT NULL,
    Website varchar(265) NOT NULL,
    Description text NOT NULL,
    Founded int NOT NULL,
    NumberOfEmployees int NOT NULL,
    CountryId varchar(36) FOREIGN KEY REFERENCES [Country](Id) ON DELETE SET NULL,
    UNIQUE(OrganizationId, DeletedAt)
);

CREATE TABLE [Industry](
    Id varchar(36) NOT NULL PRIMARY KEY,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
    IndustryName varchar(128) NOT NULL,
    UNIQUE(IndustryName, DeletedAt)
);

CREATE TABLE [IndustryOrganization](
    OrganizationId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Organization](Id) ON DELETE CASCADE,
    IndustryId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Industry](Id) ON DELETE CASCADE,
    PRIMARY KEY (OrganizationId, IndustryId)
);

CREATE TABLE [User](
    Id varchar(36) NOT NULL PRIMARY KEY,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
    Username varchar(64) NOT NULL,
    PassHash text NOT NULL,
    Salt text NOT NULL,
    UserRoleId varchar(36) NULL FOREIGN KEY REFERENCES [UserRole](Id) ON DELETE SET NULL,
    UNIQUE(Username, DeletedAt)
);

CREATE TABLE [UserRole](
    Id varchar(36) NOT NULL PRIMARY KEY,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
    RoleName varchar(16) NOT NULL,
    UNIQUE(RoleName, DeletedAt)
);