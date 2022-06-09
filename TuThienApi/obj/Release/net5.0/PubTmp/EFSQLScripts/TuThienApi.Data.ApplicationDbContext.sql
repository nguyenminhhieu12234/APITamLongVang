IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Articals] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [ProjectId] int NOT NULL,
        [FileId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Articals] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Avatar] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [Type] int NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [IsAdmin] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NULL,
        [FileId] int NOT NULL,
        [Type] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [EmailContents] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [SystemConfigId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_EmailContents] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Expenses] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Amount] decimal(18,2) NOT NULL,
        [FileId] int NOT NULL,
        [ProcessId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Expenses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [FavoriteProjectEntities] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ProjectId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_FavoriteProjectEntities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Files] (
        [Id] int NOT NULL IDENTITY,
        [FileName] nvarchar(max) NULL,
        [FilePath] nvarchar(max) NULL,
        [FriendlyUrl] nvarchar(max) NULL,
        [Note] nvarchar(max) NULL,
        [ProcessId] int NOT NULL,
        [ProjectId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Files] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Logs] (
        [Id] int NOT NULL IDENTITY,
        [RequestType] nvarchar(max) NULL,
        [RequestBody] nvarchar(max) NULL,
        [QueryString] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        [ResponeTime] nvarchar(max) NULL,
        [ErrorMessage] nvarchar(max) NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Logs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [News] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [ShortDescription] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [FriendlyUrl] nvarchar(max) NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_News] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [NewsCategories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryId] int NOT NULL,
        [NewsId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_NewsCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Processes] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [ShortDescription] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [ProjectId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Processes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [ProjectCategories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryId] int NOT NULL,
        [ProjectId] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_ProjectCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [ShortDescription] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [FriendlyUrl] nvarchar(max) NULL,
        [Summary] nvarchar(max) NULL,
        [ProblemToAddress] nvarchar(max) NULL,
        [Solution] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [Impact] nvarchar(max) NULL,
        [EndDate] datetime2 NOT NULL,
        [AddressContract] nvarchar(max) NULL,
        [Currency] nvarchar(max) NULL,
        [AmountNow] decimal(18,2) NOT NULL,
        [AmountNeed] decimal(18,2) NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [Reclaims] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [CreateUserEmail] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Reclaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [SystemConfigs] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [EmailPassword] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Facebook] nvarchar(max) NULL,
        [Youtube] nvarchar(max) NULL,
        [Instagram] nvarchar(max) NULL,
        [Medium] nvarchar(max) NULL,
        [CreateUser] int NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [UpdateUser] int NOT NULL,
        [UpdateTime] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_SystemConfigs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407032610_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220407032610_init', N'5.0.13');
END;
GO

COMMIT;
GO

