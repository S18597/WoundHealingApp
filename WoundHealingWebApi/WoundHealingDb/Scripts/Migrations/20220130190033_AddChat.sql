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