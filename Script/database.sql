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

CREATE TABLE [Brands] (
    [BrandId] int NOT NULL IDENTITY,
    [Name] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([BrandId])
);
GO

CREATE TABLE [DeviceModels] (
    [DeviceModelId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [BrandId] int NOT NULL,
    CONSTRAINT [PK_DeviceModels] PRIMARY KEY ([DeviceModelId]),
    CONSTRAINT [FK_DeviceModels_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([BrandId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Devices] (
    [DeviceId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [DeviceType] int NOT NULL,
    [Status] int NOT NULL,
    [DeviceModelId] int NOT NULL,
    CONSTRAINT [PK_Devices] PRIMARY KEY ([DeviceId]),
    CONSTRAINT [FK_Devices_DeviceModels_DeviceModelId] FOREIGN KEY ([DeviceModelId]) REFERENCES [DeviceModels] ([DeviceModelId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MaintenanceRecords] (
    [MaintenanceRecordId] int NOT NULL IDENTITY,
    [DeviceId] int NOT NULL,
    [Data] datetime2 NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Technician] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_MaintenanceRecords] PRIMARY KEY ([MaintenanceRecordId]),
    CONSTRAINT [FK_MaintenanceRecords_Devices_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Devices] ([DeviceId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_DeviceModels_BrandId] ON [DeviceModels] ([BrandId]);
GO

CREATE INDEX [IX_Devices_DeviceModelId] ON [Devices] ([DeviceModelId]);
GO

CREATE INDEX [IX_MaintenanceRecords_DeviceId] ON [MaintenanceRecords] ([DeviceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251230001632_First Migration', N'8.0.3');
GO

COMMIT;
GO

