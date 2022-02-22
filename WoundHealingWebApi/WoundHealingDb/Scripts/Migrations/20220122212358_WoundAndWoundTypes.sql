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