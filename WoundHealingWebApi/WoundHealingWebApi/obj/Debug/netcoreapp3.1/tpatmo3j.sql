IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220122100541_Initial', N'3.1.22');

GO

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

CREATE TABLE [PainLevel] (
    [PainLevelId] int NOT NULL IDENTITY,
    [PainLevelName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_PainLevel_PainLevelId] PRIMARY KEY ([PainLevelId])
);

GO

CREATE TABLE [PainType] (
    [PainTypeId] int NOT NULL IDENTITY,
    [PainTypeName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_PainType_PainTypeId] PRIMARY KEY ([PainTypeId])
);

GO

CREATE TABLE [SurroundingSkin] (
    [SurroundingSkinId] int NOT NULL IDENTITY,
    [SurroundingSkinName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_SurroundingSkin_SurroundingSkinId] PRIMARY KEY ([SurroundingSkinId])
);

GO

CREATE TABLE [WoundBleeding] (
    [WoundBleedingId] int NOT NULL IDENTITY,
    [WoundBleedingName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundBleeding_WoundBleedingId] PRIMARY KEY ([WoundBleedingId])
);

GO

CREATE TABLE [WoundColor] (
    [WoundColorId] int NOT NULL IDENTITY,
    [WoundColorName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundColor_WoundColorId] PRIMARY KEY ([WoundColorId])
);

GO

CREATE TABLE [WoundExudate] (
    [WoundExudateId] int NOT NULL IDENTITY,
    [WoundExudateName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundExudate_WoundExudateId] PRIMARY KEY ([WoundExudateId])
);

GO

CREATE TABLE [WoundLocation] (
    [WoundLocationId] int NOT NULL IDENTITY,
    [WoundLocationName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundLocation_WoundLocationId] PRIMARY KEY ([WoundLocationId])
);

GO

CREATE TABLE [WoundOdor] (
    [WoundOdorId] int NOT NULL IDENTITY,
    [WoundOdorName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundOdor_WoundOdorId] PRIMARY KEY ([WoundOdorId])
);

GO

CREATE TABLE [WoundSize] (
    [WoundSizeId] int NOT NULL IDENTITY,
    [WoundSizeName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundSize_WoundSizeId] PRIMARY KEY ([WoundSizeId])
);

GO

CREATE TABLE [WoundType] (
    [WoundTypeId] int NOT NULL IDENTITY,
    [WoundTypeName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WoundType_WoundTypeId] PRIMARY KEY ([WoundTypeId])
);

GO

CREATE TABLE [Wound] (
    [WoundId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [WoundTypeId] int NOT NULL,
    [WoundLocationId] int NOT NULL,
    [WoundSizeId] int NOT NULL,
    [WoundColorId] int NOT NULL,
    [WoundOdorId] int NOT NULL,
    [WoundExudateId] int NOT NULL,
    [WoundBleedingId] int NOT NULL,
    [SurroundingSkinId] int NOT NULL,
    [PainTypeId] int NOT NULL,
    [PainLevelId] int NOT NULL,
    [WoundRegisterDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Wound_WoundId] PRIMARY KEY ([WoundId]),
    CONSTRAINT [FK_Wound_PainLevel_PainLevelId] FOREIGN KEY ([PainLevelId]) REFERENCES [PainLevel] ([PainLevelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_PainType_PainTypeId] FOREIGN KEY ([PainTypeId]) REFERENCES [PainType] ([PainTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_SurroundingSkin_SurroundingSkinId] FOREIGN KEY ([SurroundingSkinId]) REFERENCES [SurroundingSkin] ([SurroundingSkinId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundBleeding_WoundBleedingId] FOREIGN KEY ([WoundBleedingId]) REFERENCES [WoundBleeding] ([WoundBleedingId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundColor_WoundColorId] FOREIGN KEY ([WoundColorId]) REFERENCES [WoundColor] ([WoundColorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundExudate_WoundExudateId] FOREIGN KEY ([WoundExudateId]) REFERENCES [WoundExudate] ([WoundExudateId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundLocation_WoundLocationId] FOREIGN KEY ([WoundLocationId]) REFERENCES [WoundLocation] ([WoundLocationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundOdor_WoundOdorId] FOREIGN KEY ([WoundOdorId]) REFERENCES [WoundOdor] ([WoundOdorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundSize_WoundSizeId] FOREIGN KEY ([WoundSizeId]) REFERENCES [WoundSize] ([WoundSizeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wound_WoundType_WoundTypeId] FOREIGN KEY ([WoundTypeId]) REFERENCES [WoundType] ([WoundTypeId]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_Wound_PainLevelId] ON [Wound] ([PainLevelId]);

GO

CREATE UNIQUE INDEX [IX_Wound_PainTypeId] ON [Wound] ([PainTypeId]);

GO

CREATE UNIQUE INDEX [IX_Wound_SurroundingSkinId] ON [Wound] ([SurroundingSkinId]);

GO

CREATE INDEX [IX_Wound_UserId] ON [Wound] ([UserId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundBleedingId] ON [Wound] ([WoundBleedingId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundColorId] ON [Wound] ([WoundColorId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundExudateId] ON [Wound] ([WoundExudateId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundLocationId] ON [Wound] ([WoundLocationId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundOdorId] ON [Wound] ([WoundOdorId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundSizeId] ON [Wound] ([WoundSizeId]);

GO

CREATE UNIQUE INDEX [IX_Wound_WoundTypeId] ON [Wound] ([WoundTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220122212358_WoundAndWoundTypes', N'3.1.22');

GO

DROP INDEX [IX_Wound_PainLevelId] ON [Wound];

GO

DROP INDEX [IX_Wound_PainTypeId] ON [Wound];

GO

DROP INDEX [IX_Wound_SurroundingSkinId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundBleedingId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundColorId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundExudateId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundLocationId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundOdorId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundSizeId] ON [Wound];

GO

DROP INDEX [IX_Wound_WoundTypeId] ON [Wound];

GO

CREATE INDEX [IX_Wound_PainLevelId] ON [Wound] ([PainLevelId]);

GO

CREATE INDEX [IX_Wound_PainTypeId] ON [Wound] ([PainTypeId]);

GO

CREATE INDEX [IX_Wound_SurroundingSkinId] ON [Wound] ([SurroundingSkinId]);

GO

CREATE INDEX [IX_Wound_WoundBleedingId] ON [Wound] ([WoundBleedingId]);

GO

CREATE INDEX [IX_Wound_WoundColorId] ON [Wound] ([WoundColorId]);

GO

CREATE INDEX [IX_Wound_WoundExudateId] ON [Wound] ([WoundExudateId]);

GO

CREATE INDEX [IX_Wound_WoundLocationId] ON [Wound] ([WoundLocationId]);

GO

CREATE INDEX [IX_Wound_WoundOdorId] ON [Wound] ([WoundOdorId]);

GO

CREATE INDEX [IX_Wound_WoundSizeId] ON [Wound] ([WoundSizeId]);

GO

CREATE INDEX [IX_Wound_WoundTypeId] ON [Wound] ([WoundTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220123134107_RepairWoundIndexes', N'3.1.22');

GO

CREATE TABLE [Treatment] (
    [TreatmentId] int NOT NULL IDENTITY,
    [WoundId] int NOT NULL,
    [DoctorId] int NOT NULL,
    [PatientId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Treatment_TreatmentId] PRIMARY KEY ([TreatmentId]),
    CONSTRAINT [FK_Treatment_User_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Treatment_User_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Treatment_Wound_WoundId] FOREIGN KEY ([WoundId]) REFERENCES [Wound] ([WoundId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Appointment] (
    [AppointmentId] int NOT NULL IDENTITY,
    [TreatmentId] int NOT NULL,
    [AppointmentNotes] nvarchar(1000) NULL,
    [AppointmentDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Appointment_AppointmentId] PRIMARY KEY ([AppointmentId]),
    CONSTRAINT [FK_Appointment_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([TreatmentId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Appointment_TreatmentId] ON [Appointment] ([TreatmentId]);

GO

CREATE INDEX [IX_Treatment_DoctorId] ON [Treatment] ([DoctorId]);

GO

CREATE INDEX [IX_Treatment_PatientId] ON [Treatment] ([PatientId]);

GO

CREATE UNIQUE INDEX [IX_Treatment_WoundId] ON [Treatment] ([WoundId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220123155511_TreatmentAndAppointment', N'3.1.22');

GO

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

CREATE TABLE [WoundPhoto] (
    [WoundPhotoId] int NOT NULL IDENTITY,
    [WoundId] int NOT NULL,
    [Filename] nvarchar(500) NOT NULL,
    [FileData] varbinary(max) NOT NULL,
    CONSTRAINT [PK_WoundPhoto_WoundPhotoId] PRIMARY KEY ([WoundPhotoId]),
    CONSTRAINT [FK_WoundPhoto_Wound_WoundId] FOREIGN KEY ([WoundId]) REFERENCES [Wound] ([WoundId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_WoundPhoto_WoundId] ON [WoundPhoto] ([WoundId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220127200426_AddWoundPhoto', N'3.1.22');

GO

CREATE TABLE [Chat] (
    [ChatId] int NOT NULL IDENTITY,
    [PatientId] int NOT NULL,
    [DoctorId] int NOT NULL,
    CONSTRAINT [PK_Chat_ChatId] PRIMARY KEY ([ChatId]),
    CONSTRAINT [FK_Chat_User_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [User] ([UserId]),
    CONSTRAINT [FK_Chat_User_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [User] ([UserId])
);

GO

CREATE TABLE [ChatMessage] (
    [ChatMessageId] int NOT NULL IDENTITY,
    [ChatId] int NOT NULL,
    [UserId] int NOT NULL,
    [Message] nvarchar(1000) NOT NULL,
    [MessageDate] datetime2 NOT NULL,
    CONSTRAINT [PK_ChatMessage_ChatMessageId] PRIMARY KEY ([ChatMessageId]),
    CONSTRAINT [FK_ChatMessage_Chat_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [Chat] ([ChatId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChatMessage_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId])
);

GO

CREATE INDEX [IX_Chat_DoctorId] ON [Chat] ([DoctorId]);

GO

CREATE INDEX [IX_Chat_PatientId] ON [Chat] ([PatientId]);

GO

CREATE INDEX [IX_ChatMessage_ChatId] ON [ChatMessage] ([ChatId]);

GO

CREATE INDEX [IX_ChatMessage_UserId] ON [ChatMessage] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220130190033_AddChat', N'3.1.22');

GO

