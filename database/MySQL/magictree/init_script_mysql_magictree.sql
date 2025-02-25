# =============== CREATE DATABASE & USERS =============== #
CREATE DATABASE IF NOT EXISTS MagicTree;

-- CREATE USER AND GRANT PERMISSIONS
-- CREATE USER IF NOT EXISTS 'mt_admin'@'localhost' IDENTIFIED BY '12345678x@X';
-- CREATE USER IF NOT EXISTS 'mt_dev'@'localhost' IDENTIFIED BY '12345678x@X';
-- CREATE USER IF NOT EXISTS 'mt_qa'@'localhost' IDENTIFIED BY '12345678x@X';
-- CREATE USER IF NOT EXISTS 'mt_coporate'@'localhost' IDENTIFIED BY '12345678x@X';

-- GRANT ALL PRIVILEGES ON MagicTree.* TO 'mt_admin'@'localhost';
-- GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, DROP, EXECUTE, CREATE ROUTINE, ALTER ROUTINE, CREATE VIEW, CREATE TEMPORARY TABLES, TRIGGER ON MagicTree.* TO 'mt_dev'@'localhost';
-- GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON MagicTree.* TO 'mt_qa'@'localhost';
-- GRANT SELECT ON MagicTree.* TO 'mt_coporate'@'localhost';
-- FLUSH PRIVILEGES;

# CHECK USER
# SELECT * FROM MYSQL.USER;

# =============== CREATE TABLES =============== #
USE MagicTree;

CREATE TABLE IF NOT EXISTS Permission
(
    PermissionId	INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    Description     VARCHAR(255),
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Permission_Name UNIQUE (Name),
    CONSTRAINT FK_Permission_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_Permission_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS Role
(
    RoleId			INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    Description     VARCHAR(255),
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Role_Name UNIQUE (Name),
    CONSTRAINT FK_Role_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_Role_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS RolePermission
(
    RolePermissionId INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    RoleId			INT UNSIGNED,
    PermissionId	INT UNSIGNED,
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Role_Permissions UNIQUE (RoleId, PermissionId),
    CONSTRAINT FK_RP_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
    CONSTRAINT FK_RP_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permission(PermissionId),
    CONSTRAINT FK_RP_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_RP_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS User
(
    UserId			INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    RoleId        	INT UNSIGNED,
    Username	    VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL,
    Password		VARCHAR(255) NOT NULL,
    Email			VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL,
    FirstName		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    LastName		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    Department		VARCHAR(255),
    JobTitle		VARCHAR(255),
    MobilePhone		VARCHAR(20),
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_User_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE INDEX Idx_User_Username ON User(Username);
CREATE INDEX Idx_User_Email ON User(Email);
CREATE INDEX Idx_User_MobilePhone ON User(MobilePhone);

CREATE TABLE IF NOT EXISTS RefreshToken
(
    RefreshTokenId	INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId			INT UNSIGNED NOT NULL,
    Token			VARCHAR(255) NOT NULL,
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_RT_UserId FOREIGN KEY (UserId) REFERENCES User(UserId),
    CONSTRAINT FK_RT_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_RT_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS MetadataCategory
(
	MetadataCategoryId	INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    CreatedBy		INT UNSIGNED,
    UpdatedBy		INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_MC_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_MC_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS Metadata
(
	MetadataId	    INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    S3Url  	        VARCHAR(255),
    MetadataCategoryId INT UNSIGNED,
    IsActive    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CreatedBy		INT UNSIGNED,
    UpdatedBy   	INT UNSIGNED,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_Metadata_CategoryId FOREIGN KEY (MetadataCategoryId) REFERENCES MetadataCategory(MetadataCategoryId),
    CONSTRAINT FK_Metadata_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_Metadata_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
);

CREATE TABLE IF NOT EXISTS Campaign
(
    CampaignId      INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Title           VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    Content         VARCHAR(5000) DEFAULT NULL,
    Status          ENUM('Open','Closed') DEFAULT 'Open',
    MetadataId      INT UNSIGNED NOT NULL,
    CreatedBy       INT,
    UpdatedBy       INT,
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted       TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_Campaign_MetadataId FOREIGN KEY (MetadataId) REFERENCES Metadata (MetadataId),
    CONSTRAINT FK_Campaign_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES User(UserId),
    CONSTRAINT FK_Campaign_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES User(UserId)
)


# =============== INSERT DATA =============== #
-- Inserting data into Role table
INSERT INTO Role (Name, Description, CreatedBy, CreatedDate)
VALUES	('System', 'Responsible for system', null, NOW()),
		('Admin', 'Responsible for administrative', null, NOW()),
        ('Client', 'Responsible for client', null, NOW());

-- Inserting data into User table
--  DEFAULT PASSWORD: 12345678x@X
INSERT INTO User (RoleId, Username, Password, Email, FirstName, LastName, MobilePhone, CreatedBy, CreatedDate)
VALUES	(1, 'magictree.system', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'system@magictree.com', 'System', null, '0123456789', null),
		(2, 'magictree.admin', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'admin@magictree.com', 'Admin', null, '0133456789', 1),
        (3, 'magictree.client', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'client@magictree.com', 'Client', null, '014567899', 1);

-- Inserting data into MetadataCategory table
-- Inserting data into MetadataCategory
INSERT INTO MetadataCategory (Name, CreatedBy, UpdatedBy)
VALUES  ('Thumbnail', 1),
        ('Image', 1)
        ('Audio', 1);

-- Inserting data into Metadata table
INSERT INTO Metadata (Name, S3Url, MetadataCategoryId, IsActive, CreatedBy)
VALUES  ('Metadata 1', 'http://example.com/file1', 1, 1, 1),
        ('Metadata 2', 'http://example.com/file2', 2, 1, 1),
        ('Metadata 3', 'http://example.com/file3', 3, 0, 1);

-- Inserting data into Campaign table
INSERT INTO Campaign (Title, Content, Status, MetadataId, CreatedBy)
VALUES  ('Campaign 1', 'Content 1', 'Open', 1, 1),
        ('Campaign 2', 'Content 2', 'Open', 2, 1),
        ('Campaign 3', 'Content 3', 'Closed', 3, 1);
