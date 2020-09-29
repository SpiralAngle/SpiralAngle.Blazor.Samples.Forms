IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [GameSystems] (
        [GameSystemId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Version] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Publisher] nvarchar(max) NULL,
        CONSTRAINT [PK_GameSystems] PRIMARY KEY ([GameSystemId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [Items] (
        [ItemId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Weight] decimal(18,2) NOT NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId]),
        CONSTRAINT [FK_Items_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [Races] (
        [RaceId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        [GameSystemId1] uniqueidentifier NULL,
        CONSTRAINT [PK_Races] PRIMARY KEY ([RaceId]),
        CONSTRAINT [FK_Races_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId]),
        CONSTRAINT [FK_Races_GameSystems_GameSystemId1] FOREIGN KEY ([GameSystemId1]) REFERENCES [GameSystems] ([GameSystemId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [Roles] (
        [RoleId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        [GameSystemId1] uniqueidentifier NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId]),
        CONSTRAINT [FK_Roles_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId]),
        CONSTRAINT [FK_Roles_GameSystems_GameSystemId1] FOREIGN KEY ([GameSystemId1]) REFERENCES [GameSystems] ([GameSystemId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [SkillGroups] (
        [SkillGroupId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_SkillGroups] PRIMARY KEY ([SkillGroupId]),
        CONSTRAINT [FK_SkillGroups_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [Creatures] (
        [CreatureId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        [RaceId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [Strength] int NOT NULL,
        [Dexterity] int NOT NULL,
        [Constitution] int NOT NULL,
        [Intelligence] int NOT NULL,
        [Wisdom] int NOT NULL,
        [Charisma] int NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Creatures] PRIMARY KEY ([CreatureId]),
        CONSTRAINT [FK_Creatures_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId]),
        CONSTRAINT [FK_Creatures_Races_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Races] ([RaceId]),
        CONSTRAINT [FK_Creatures_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [Skills] (
        [SkillId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [SkillGroupId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([SkillId]),
        CONSTRAINT [FK_Skills_SkillGroups_SkillGroupId] FOREIGN KEY ([SkillGroupId]) REFERENCES [SkillGroups] ([SkillGroupId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [InventoryItems] (
        [ItemInventoryId] uniqueidentifier NOT NULL,
        [ItemId1] uniqueidentifier NOT NULL,
        [ItemId] uniqueidentifier NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [CreatureId] uniqueidentifier NULL,
        [CreatureId1] uniqueidentifier NULL,
        CONSTRAINT [PK_InventoryItems] PRIMARY KEY ([ItemInventoryId]),
        CONSTRAINT [FK_InventoryItems_Creatures_CreatureId] FOREIGN KEY ([CreatureId]) REFERENCES [Creatures] ([CreatureId]),
        CONSTRAINT [FK_InventoryItems_Creatures_CreatureId1] FOREIGN KEY ([CreatureId1]) REFERENCES [Creatures] ([CreatureId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InventoryItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([ItemId]),
        CONSTRAINT [FK_InventoryItems_Items_ItemId1] FOREIGN KEY ([ItemId1]) REFERENCES [Items] ([ItemId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE TABLE [CreatureSkills] (
        [CreatureSkillId] uniqueidentifier NOT NULL,
        [CreatureId] uniqueidentifier NOT NULL,
        [SkillId] uniqueidentifier NOT NULL,
        [Rank] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_CreatureSkills] PRIMARY KEY ([CreatureSkillId]),
        CONSTRAINT [FK_CreatureSkills_Creatures_CreatureId] FOREIGN KEY ([CreatureId]) REFERENCES [Creatures] ([CreatureId]),
        CONSTRAINT [FK_CreatureSkills_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skills] ([SkillId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Creatures_GameSystemId] ON [Creatures] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Creatures_RaceId] ON [Creatures] ([RaceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Creatures_RoleId] ON [Creatures] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_CreatureSkills_CreatureId] ON [CreatureSkills] ([CreatureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_CreatureSkills_SkillId] ON [CreatureSkills] ([SkillId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_CreatureId] ON [InventoryItems] ([CreatureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_CreatureId1] ON [InventoryItems] ([CreatureId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_ItemId] ON [InventoryItems] ([ItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_InventoryItems_ItemId1] ON [InventoryItems] ([ItemId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Items_GameSystemId] ON [Items] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Races_GameSystemId] ON [Races] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Races_GameSystemId1] ON [Races] ([GameSystemId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Roles_GameSystemId] ON [Roles] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Roles_GameSystemId1] ON [Roles] ([GameSystemId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_SkillGroups_GameSystemId] ON [SkillGroups] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Skills_SkillGroupId] ON [Skills] ([SkillGroupId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920034320_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200920034320_InitialCreate', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE TABLE [Attributes] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [GameSystemId1] uniqueidentifier NULL,
        [GameSystemId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Attributes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Attributes_GameSystems_GameSystemId] FOREIGN KEY ([GameSystemId]) REFERENCES [GameSystems] ([GameSystemId]),
        CONSTRAINT [FK_Attributes_GameSystems_GameSystemId1] FOREIGN KEY ([GameSystemId1]) REFERENCES [GameSystems] ([GameSystemId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE TABLE [RaceSkillModifiers] (
        [Id] uniqueidentifier NOT NULL,
        [SkillId] uniqueidentifier NOT NULL,
        [Modifier] decimal(18,2) NOT NULL,
        [RaceId] uniqueidentifier NOT NULL,
        [RaceId1] uniqueidentifier NULL,
        CONSTRAINT [PK_RaceSkillModifiers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RaceSkillModifiers_Races_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Races] ([RaceId]) ON DELETE CASCADE,
        CONSTRAINT [FK_RaceSkillModifiers_Races_RaceId1] FOREIGN KEY ([RaceId1]) REFERENCES [Races] ([RaceId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RaceSkillModifiers_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skills] ([SkillId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE TABLE [RaceAttributeModifiers] (
        [Id] uniqueidentifier NOT NULL,
        [AttributeId] uniqueidentifier NOT NULL,
        [Modifier] decimal(18,2) NOT NULL,
        [RaceId] uniqueidentifier NOT NULL,
        [RaceId1] uniqueidentifier NULL,
        CONSTRAINT [PK_RaceAttributeModifiers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RaceAttributeModifiers_Attributes_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [Attributes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RaceAttributeModifiers_Races_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Races] ([RaceId]) ON DELETE CASCADE,
        CONSTRAINT [FK_RaceAttributeModifiers_Races_RaceId1] FOREIGN KEY ([RaceId1]) REFERENCES [Races] ([RaceId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_Attributes_GameSystemId] ON [Attributes] ([GameSystemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_Attributes_GameSystemId1] ON [Attributes] ([GameSystemId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceAttributeModifiers_AttributeId] ON [RaceAttributeModifiers] ([AttributeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceAttributeModifiers_RaceId] ON [RaceAttributeModifiers] ([RaceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceAttributeModifiers_RaceId1] ON [RaceAttributeModifiers] ([RaceId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceSkillModifiers_RaceId] ON [RaceSkillModifiers] ([RaceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceSkillModifiers_RaceId1] ON [RaceSkillModifiers] ([RaceId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    CREATE INDEX [IX_RaceSkillModifiers_SkillId] ON [RaceSkillModifiers] ([SkillId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926205216_AddAttributes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200926205216_AddAttributes', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927030707_AddMinMaxToAttribute')
BEGIN
    ALTER TABLE [Attributes] ADD [Maximum] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927030707_AddMinMaxToAttribute')
BEGIN
    ALTER TABLE [Attributes] ADD [Minimum] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927030707_AddMinMaxToAttribute')
BEGIN
    ALTER TABLE [Attributes] ADD [ModifiedMaximum] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927030707_AddMinMaxToAttribute')
BEGIN
    ALTER TABLE [Attributes] ADD [ModifiedMinimum] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927030707_AddMinMaxToAttribute')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200927030707_AddMinMaxToAttribute', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Charisma');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Charisma];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Constitution');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Constitution];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Dexterity');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Dexterity];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Intelligence');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Intelligence];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Strength');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Strength];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Creatures]') AND [c].[name] = N'Wisdom');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Creatures] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Creatures] DROP COLUMN [Wisdom];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    EXEC sp_rename N'[RaceSkillModifiers].[Id]', N'RaceSkillModifierId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    EXEC sp_rename N'[RaceAttributeModifiers].[Id]', N'RaceAttributeModifierId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    EXEC sp_rename N'[Attributes].[Id]', N'AttributeId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    ALTER TABLE [Attributes] ADD [Order] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    CREATE TABLE [CreatureAttributes] (
        [CreatureAttribute] uniqueidentifier NOT NULL,
        [CreatureId] uniqueidentifier NOT NULL,
        [AttributeId] uniqueidentifier NOT NULL,
        [Value] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_CreatureAttributes] PRIMARY KEY ([CreatureAttribute]),
        CONSTRAINT [FK_CreatureAttributes_Attributes_AttributeId] FOREIGN KEY ([AttributeId]) REFERENCES [Attributes] ([AttributeId]),
        CONSTRAINT [FK_CreatureAttributes_Creatures_CreatureId] FOREIGN KEY ([CreatureId]) REFERENCES [Creatures] ([CreatureId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    CREATE INDEX [IX_CreatureAttributes_AttributeId] ON [CreatureAttributes] ([AttributeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    CREATE INDEX [IX_CreatureAttributes_CreatureId] ON [CreatureAttributes] ([CreatureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200927220321_AddCreatureAttributes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200927220321_AddCreatureAttributes', N'3.1.8');
END;

GO