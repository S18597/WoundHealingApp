CREATE TABLE [Treatment] (
    [TreatmentId] int NOT NULL IDENTITY,
    [WoundId] int NOT NULL,
    [DoctorId] int NOT NULL,
    [PatientId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Treatment_TreatmentId] PRIMARY KEY ([TreatmentId]),
    CONSTRAINT [FK_Treatment_User_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Treatment_User_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION,
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