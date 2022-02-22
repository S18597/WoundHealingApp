-- WoundType --
insert into dbo.WoundType(WoundTypeName) values ('Incision');
insert into dbo.WoundType(WoundTypeName) values ('Laceration');
insert into dbo.WoundType(WoundTypeName) values ('Abrasion');
insert into dbo.WoundType(WoundTypeName) values ('Puncture');
insert into dbo.WoundType(WoundTypeName) values ('Avulsion');
insert into dbo.WoundType(WoundTypeName) values ('Amputation');
insert into dbo.WoundType(WoundTypeName) values ('Surgical');

-- WoundLocation --
insert into dbo.WoundLocation(WoundLocationName) values ('Head');
insert into dbo.WoundLocation(WoundLocationName) values ('Neck');
insert into dbo.WoundLocation(WoundLocationName) values ('Chest');
insert into dbo.WoundLocation(WoundLocationName) values ('Abdomen');
insert into dbo.WoundLocation(WoundLocationName) values ('Upper arm');
insert into dbo.WoundLocation(WoundLocationName) values ('Lower arm');
insert into dbo.WoundLocation(WoundLocationName) values ('Hand');
insert into dbo.WoundLocation(WoundLocationName) values ('Upper leg');
insert into dbo.WoundLocation(WoundLocationName) values ('Lower leg');
insert into dbo.WoundLocation(WoundLocationName) values ('Foot');
insert into dbo.WoundLocation(WoundLocationName) values ('Other');

-- WoundSize --
insert into dbo.WoundSize(WoundSizeName) values ('Small');
insert into dbo.WoundSize(WoundSizeName) values ('Medium');
insert into dbo.WoundSize(WoundSizeName) values ('Large');

-- WoundColor --
insert into dbo.WoundColor(WoundColorName) values ('Wound healed');
insert into dbo.WoundColor(WoundColorName) values ('Pink');
insert into dbo.WoundColor(WoundColorName) values ('Red');
insert into dbo.WoundColor(WoundColorName) values ('Yellow');
insert into dbo.WoundColor(WoundColorName) values ('Black');
insert into dbo.WoundColor(WoundColorName) values ('Green');

-- WoundOdor --
insert into dbo.WoundOdor(WoundOdorName) values ('Yes');
insert into dbo.WoundOdor(WoundOdorName) values ('No');

-- WoundExudate --
insert into dbo.WoundExudate(WoundExudateName) values ('None');
insert into dbo.WoundExudate(WoundExudateName) values ('Low');
insert into dbo.WoundExudate(WoundExudateName) values ('Moderate');
insert into dbo.WoundExudate(WoundExudateName) values ('High');

-- WoundBleeding --
insert into dbo.WoundBleeding(WoundBleedingName) values ('None');
insert into dbo.WoundBleeding(WoundBleedingName) values ('Low');
insert into dbo.WoundBleeding(WoundBleedingName) values ('Moderate');
insert into dbo.WoundBleeding(WoundBleedingName) values ('High');

-- SurroundingSkin --
insert into dbo.SurroundingSkin(SurroundingSkinName) values ('Healthy');
insert into dbo.SurroundingSkin(SurroundingSkinName) values ('Dry');
insert into dbo.SurroundingSkin(SurroundingSkinName) values ('Scaly');
insert into dbo.SurroundingSkin(SurroundingSkinName) values ('Pale');

-- PainType --
insert into dbo.PainType(PainTypeName) values ('Chronic');
insert into dbo.PainType(PainTypeName) values ('On movement');
insert into dbo.PainType(PainTypeName) values ('On dressing change');
insert into dbo.PainType(PainTypeName) values ('None');

-- PainLevel --
insert into dbo.PainLevel(PainLevelName) values ('None');
insert into dbo.PainLevel(PainLevelName) values ('Low');
insert into dbo.PainLevel(PainLevelName) values ('Moderate');
insert into dbo.PainLevel(PainLevelName) values ('High');