CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Brands` (
    `BrandId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(40) NOT NULL,
    PRIMARY KEY (`BrandId`)
);

CREATE TABLE `DeviceModels` (
    `DeviceModelId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(50) NOT NULL,
    `BrandId` int NOT NULL,
    PRIMARY KEY (`DeviceModelId`),
    CONSTRAINT `FK_DeviceModels_Brands_BrandId` FOREIGN KEY (`BrandId`) REFERENCES `Brands` (`BrandId`) ON DELETE RESTRICT
);

CREATE TABLE `Devices` (
    `DeviceId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(50) NOT NULL,
    `DeviceType` int NOT NULL,
    `Status` int NOT NULL,
    `DeviceModelId` int NOT NULL,
    PRIMARY KEY (`DeviceId`),
    CONSTRAINT `FK_Devices_DeviceModels_DeviceModelId` FOREIGN KEY (`DeviceModelId`) REFERENCES `DeviceModels` (`DeviceModelId`) ON DELETE RESTRICT
);

CREATE TABLE `MaintenanceRecords` (
    `MaintenanceRecordId` int NOT NULL AUTO_INCREMENT,
    `DeviceId` int NOT NULL,
    `Data` datetime(6) NOT NULL,
    `Description` varchar(500) NOT NULL,
    `Technician` varchar(100) NOT NULL,
    PRIMARY KEY (`MaintenanceRecordId`),
    CONSTRAINT `FK_MaintenanceRecords_Devices_DeviceId` FOREIGN KEY (`DeviceId`) REFERENCES `Devices` (`DeviceId`) ON DELETE CASCADE
);

CREATE INDEX `IX_DeviceModels_BrandId` ON `DeviceModels` (`BrandId`);

CREATE INDEX `IX_Devices_DeviceModelId` ON `Devices` (`DeviceModelId`);

CREATE INDEX `IX_MaintenanceRecords_DeviceId` ON `MaintenanceRecords` (`DeviceId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260106192119_Initial MySQL migration', '8.0.3');

COMMIT;

