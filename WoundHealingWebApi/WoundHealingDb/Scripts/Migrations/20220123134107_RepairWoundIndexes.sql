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