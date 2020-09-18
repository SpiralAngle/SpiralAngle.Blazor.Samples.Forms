IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [Creatures] (
        [CreatureId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Race] nvarchar(max) NOT NULL,
        [Class] nvarchar(max) NOT NULL,
        [Strength] int NOT NULL,
        [Dexterity] int NOT NULL,
        [Constitution] int NOT NULL,
        [Intelligence] int NOT NULL,
        [Wisdom] int NOT NULL,
        [Charisma] int NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Creatures] PRIMARY KEY ([CreatureId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [Items] (
        [ItemId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Weight] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [SkillGroups] (
        [SkillGroupId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SkillGroups] PRIMARY KEY ([SkillGroupId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [InventoryItems] (
        [ItemInventoryId] uniqueidentifier NOT NULL,
        [ItemId] uniqueidentifier NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [CreatureId] uniqueidentifier NULL,
        CONSTRAINT [PK_InventoryItems] PRIMARY KEY ([ItemInventoryId]),
        CONSTRAINT [FK_InventoryItems_Creatures_CreatureId] FOREIGN KEY ([CreatureId]) REFERENCES [Creatures] ([CreatureId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InventoryItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([ItemId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [Skills] (
        [SkillId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [SkillGroupId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([SkillId]),
        CONSTRAINT [FK_Skills_SkillGroups_SkillGroupId] FOREIGN KEY ([SkillGroupId]) REFERENCES [SkillGroups] ([SkillGroupId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE TABLE [CreatureSkills] (
        [CreatureSkillId] uniqueidentifier NOT NULL,
        [SkillId] uniqueidentifier NOT NULL,
        [Rank] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_CreatureSkills] PRIMARY KEY ([CreatureSkillId]),
        CONSTRAINT [FK_CreatureSkills_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skills] ([SkillId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE INDEX [IX_CreatureSkills_SkillId] ON [CreatureSkills] ([SkillId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_CreatureId] ON [InventoryItems] ([CreatureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_ItemId] ON [InventoryItems] ([ItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    CREATE INDEX [IX_Skills_SkillGroupId] ON [Skills] ([SkillGroupId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918213755_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200918213755_InitialCreate', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    ALTER TABLE [Items] ADD [GameSystemId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    ALTER TABLE [Creatures] ADD [GameSystemId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    CREATE TABLE [GameSystem] (
        [GameSystemId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Version] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_GameSystem] PRIMARY KEY ([GameSystemId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    CREATE INDEX [IX_Items_GameSystemId] ON [Items] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    CREATE INDEX [IX_Creatures_GameSystemId] ON [Creatures] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    ALTER TABLE [Creatures] ADD CONSTRAINT [FK_Creatures_GameSystem_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystem] ([GameSystemId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    ALTER TABLE [Items] ADD CONSTRAINT [FK_Items_GameSystem_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystem] ([GameSystemId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918222305_ConnectGameSystemToOtherEntities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200918222305_ConnectGameSystemToOtherEntities', N'3.1.8');
END;

GO

