CREATE TABLE [User] (
    [UserId] int NOT NULL IDENTITY,
    [Firstname] nvarchar(50) NOT NULL,
    [Lastname] nvarchar(50) NOT NULL,
    [Pesel] nvarchar(11) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [PhoneNumber] nvarchar(20) NOT NULL,
    [EmailAddress] nvarchar(50) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [IsPatient] bit NOT NULL,
    [IsDoctor] bit NOT NULL,
    CONSTRAINT [PK_User_UserId] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [Auth] (
    [UserId] int NOT NULL,
    [Salt] nvarchar(10) NOT NULL,
    [Hash] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Auth_UserId] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Auth_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220122104837_UserAndAuthModel', N'3.1.22');

GO