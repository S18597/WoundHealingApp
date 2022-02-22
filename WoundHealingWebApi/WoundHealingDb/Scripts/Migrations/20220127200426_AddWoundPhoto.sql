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