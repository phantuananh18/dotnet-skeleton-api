# =============== CREATE DATABASE & USERS =============== #
-- CREATE DATABASE IF NOT EXISTS SkeletonDB;

CREATE USER IF NOT EXISTS 'admin'@'%' IDENTIFIED BY '12345678x@X';
GRANT ALL PRIVILEGES ON SkeletonDB.* TO 'admin'@'%' WITH GRANT OPTION;
FLUSH PRIVILEGES;

# CHECK USER
# SELECT * FROM MYSQL.USER;

# =============== CREATE TABLES =============== #
USE MagicTree;

CREATE TABLE IF NOT EXISTS Permission
(
    PermissionId	INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    Description     VARCHAR(255),
    CreatedBy   	INT(10),
    UpdatedBy   	INT(10),
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Permission_Name UNIQUE (Name)
);

CREATE TABLE IF NOT EXISTS Role
(
    RoleId			INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name        	VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL,
    Description     VARCHAR(255),
    CreatedBy   	INT(10),
    UpdatedBy   	INT(10),
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Role_Name UNIQUE (Name)
);

CREATE TABLE IF NOT EXISTS RolePermission
(
    RolePermissionId INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    RoleId			INT(10) UNSIGNED,
    PermissionId	INT(10) UNSIGNED,
    CreatedBy   	INT(10),
    UpdatedBy   	INT(10),
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT UQ_Role_Permissions UNIQUE (RoleId, PermissionId),
    CONSTRAINT FK_RP_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
    CONSTRAINT FK_RP_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permission(PermissionId)
);

CREATE TABLE IF NOT EXISTS User
(
    UserId			INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    RoleId        	INT(10) UNSIGNED,
    Username	    VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL,
    Password		VARCHAR(255) NOT NULL,
    Email			VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL,
    FirstName		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    LastName		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    Department		VARCHAR(255),
    JobTitle		VARCHAR(255),
    MobilePhone		VARCHAR(20),
    CreatedBy   	INT(10),
    UpdatedBy   	INT(10),
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_User_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE INDEX Idx_User_Username ON User(Username);
CREATE INDEX Idx_User_Email ON User(Email);
CREATE INDEX Idx_User_MobilePhone ON User(MobilePhone);

CREATE TABLE IF NOT EXISTS RefreshToken
(
    RefreshTokenId	INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId			INT(10) UNSIGNED NOT NULL,
    Token			VARCHAR(255) NOT NULL,
    CreatedBy   	INT(10),
    UpdatedBy   	INT(10),
    CreatedDate 	DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 	DATETIME,
    IsDeleted    	TINYINT(1) UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_RT_UserId FOREIGN KEY (UserId) REFERENCES User(UserId)
);

# =============== INSERT DATA =============== #
-- Inserting data into Role table
INSERT INTO Role (Name, Description, CreatedBy, CreatedDate)
VALUES	('System', 'Responsible for system', null, NOW()),
		('Admin', 'Responsible for administrative', null, NOW()),
        ('Client', 'Responsible for client', null, NOW());

-- Inserting data into User table
INSERT INTO User (RoleId, Username, Password, Email, FirstName, LastName, MobilePhone, CreatedBy, CreatedDate)
VALUES	(1, 'dwf.system', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'system@dc8framework.com', 'System', null, '0123456789', null, NOW()),
		(2, 'dwf.admin', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'admin@dc8framework.com', 'Admin', null, '0133456789', 1, NOW()),
        (3, 'dwf.client', '$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy', 'client@dc8framework.com', 'Client', null, '014567899', 1, NOW());

# DEFAULT PASSWORD: 12345678x@X

CREATE TABLE IF NOT EXISTS UserAccount
(
    UserAccountId			INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId        			INT UNSIGNED,
    AuthMethod 				ENUM('OAuth', 'SSO', 'UsernamePassword', 'LDAP', 'SAML', 'OpenID', 'JWT', 'Biometric', 'MFA', 'APIKey', 'Certificate', 'SocialLogin') NOT NULL,
    OAuthProvider			VARCHAR(50) COLLATE utf8mb4_unicode_ci,
    OAuthProviderUserId		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    SSOProvider				VARCHAR(50) COLLATE utf8mb4_unicode_ci,
    SSOProviderUserId		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    TwoFactorEnabled 		TINYINT UNSIGNED DEFAULT '0',
    TwoFactorSecret 		VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    TwoFactorBackupCodes	VARCHAR(255) COLLATE utf8mb4_unicode_ci,
    CreatedBy   			INT,
    UpdatedBy   			INT,
    CreatedDate 			DATETIME DEFAULT NOW() NOT NULL,
    UpdatedDate 			DATETIME,
    IsDeleted    			TINYINT UNSIGNED NOT NULL DEFAULT '0',
    CONSTRAINT FK_UserAccount_UserId FOREIGN KEY (UserId) REFERENCES User(UserId)
);

-- Inserting data into UserAccount table
INSERT INTO UserAccount (UserId, AuthMethod, CreatedBy)
VALUES (1, 'UsernamePassword', 1);

CREATE TABLE IF NOT EXISTS SkeletonDB.Communication
(
    CommunicationId INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    SenderId INT UNSIGNED NOT NULL,
    SenderInfo VARCHAR(255) NULL,
    ReceiverId INT NULL,
    ReceiverInfo VARCHAR(255) NULL,
    CommunicationType VARCHAR(50) COLLATE utf8mb4_unicode_ci NOT NULL, -- Email, Text, Call, etc
    Direction ENUM('Outgoing', 'Incoming') NOT NULL,
    CommunicationDatetime DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Status VARCHAR(50) NOT NULL, -- Sent, Failed, Bounced
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDate DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted TINYINT UNSIGNED DEFAULT '0' NOT NULL,
    INDEX IDX_SenderId (SenderId),
    INDEX IDX_ReceiverId (ReceiverId),
    INDEX IDX_CommunicationType (CommunicationType),
    INDEX IDX_SenderId_CommunicationDatetime (SenderId, CommunicationDatetime),
    FOREIGN KEY (SenderId) REFERENCES SkeletonDB.User(UserId)
);

CREATE TABLE IF NOT EXISTS SkeletonDB.CommunicationTemplate
(
    CommunicationTemplateId INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    TemplateName VARCHAR(255) NOT NULL,
    Subject VARCHAR(255) NOT NULL,
    TemplateContentUrl VARCHAR(255) NOT NULL,
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDate DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted TINYINT UNSIGNED DEFAULT '0' NOT NULL,
    UNIQUE INDEX UQ_TemplateName (TemplateName)
);

CREATE TABLE IF NOT EXISTS SkeletonDB.EmailMetadata
(
    EmailMetadataId INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    CommunicationId INT UNSIGNED NOT NULL,
    CommunicationTemplateId INT UNSIGNED NOT NULL,
    Subject VARCHAR(255) NULL,
    ContentUrl VARCHAR(255) NULL,
    ErrorMessage VARCHAR(500) NULL,
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDate DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    IsDeleted TINYINT UNSIGNED DEFAULT '0' NOT NULL,
    INDEX IDX_CommunicationId (CommunicationId),
    FOREIGN KEY (CommunicationId) REFERENCES SkeletonDB.Communication(CommunicationId) ON DELETE CASCADE,
    FOREIGN KEY (CommunicationTemplateId) REFERENCES SkeletonDB.CommunicationTemplate(CommunicationTemplateId)
);