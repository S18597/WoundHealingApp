CREATE TABLE [MedicalData] (
    [UserId] int NOT NULL,
    [ChronicDeseases] nvarchar(1000) NULL,
    [Allergies] nvarchar(500) NULL,
    [Medication] nvarchar(500) NULL,
    [Pregnancy] bit NOT NULL,
    [Tobacco] bit NOT NULL,
    [Alcohol] bit NOT NULL,
    [Drugs] bit NOT NULL,
    CONSTRAINT [PK_MedicalData_UserId] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_MedicalData_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220127140308_AddMedicalData', N'3.1.22');

GO