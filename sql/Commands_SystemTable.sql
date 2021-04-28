IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Providers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Document] varchar(14) NOT NULL,
    [TypeProvider] int NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Addresses] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Street] varchar(200) NOT NULL,
    [Number] varchar(6) NOT NULL,
    [Complement] varchar(50) NULL,
    [ZipCode] varchar(8) NOT NULL,
    [Neighborhood] varchar(50) NOT NULL,
    [City] varchar(50) NOT NULL,
    [State] varchar(2) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(200) NULL,
    [Value] decimal(18,2) NOT NULL,
    [DateRegister] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_Addresses_ProviderId] ON [Addresses] ([ProviderId]);

GO

CREATE INDEX [IX_Products_ProviderId] ON [Products] ([ProviderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210428232123_System Tables', N'2.2.6-servicing-10079');

GO

