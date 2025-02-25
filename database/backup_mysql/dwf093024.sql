CREATE DATABASE  IF NOT EXISTS `SkeletonDB` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `SkeletonDB`;
-- MySQL dump 10.13  Distrib 8.0.16, for Win64 (x86_64)
--
-- Host: 11.11.7.81    Database: SkeletonDB
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Communication`
--

DROP TABLE IF EXISTS `Communication`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Communication` (
  `CommunicationId` int unsigned NOT NULL AUTO_INCREMENT,
  `SenderId` int unsigned NOT NULL,
  `SenderInfo` varchar(255) DEFAULT NULL,
  `ReceiverId` int DEFAULT NULL,
  `ReceiverInfo` varchar(255) DEFAULT NULL,
  `CommunicationType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Direction` enum('Outgoing','Incoming') NOT NULL,
  `CommunicationDatetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Status` varchar(50) NOT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CommunicationId`),
  KEY `IDX_SenderId` (`SenderId`),
  KEY `IDX_ReceiverId` (`ReceiverId`),
  KEY `IDX_CommunicationType` (`CommunicationType`),
  KEY `IDX_SenderId_CommunicationDatetime` (`SenderId`,`CommunicationDatetime`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Communication`
--

LOCK TABLES `Communication` WRITE;
/*!40000 ALTER TABLE `Communication` DISABLE KEYS */;
INSERT INTO `Communication` VALUES (1,1,'dc8framework@gmail.com',NULL,'daksuthang0074@gmail.com','Email','Outgoing','2024-09-30 04:36:04','Delivered',1,1,'2024-09-30 04:36:04','2024-09-30 04:36:08',0),(2,1,'dc8framework@gmail.com',NULL,'daksuthang0074@gmail.com','Email','Outgoing','2024-09-30 04:36:44','Delivered',1,1,'2024-09-30 04:36:45','2024-09-30 04:36:48',0),(3,2,'noreply.dc8framework@gmail.com',NULL,'tranlong@gmail.com','Email','Outgoing','2024-09-30 06:08:17','Delivered',1,1,'2024-09-30 06:08:17','2024-09-30 06:08:20',0),(4,2,'noreply.dc8framework@gmail.com',NULL,'tranlong123@gmail.com','Email','Outgoing','2024-09-30 06:11:19','Delivered',1,1,'2024-09-30 06:11:19','2024-09-30 06:11:22',0),(5,2,'noreply.dc8framework@gmail.com',NULL,'lehung@gmail.com','Email','Outgoing','2024-09-30 06:13:38','Delivered',1,1,'2024-09-30 06:13:38','2024-09-30 06:13:42',0),(6,2,'noreply.dc8framework@gmail.com',NULL,'trantuan@gmail.com','Email','Outgoing','2024-09-30 06:29:20','Delivered',1,1,'2024-09-30 06:29:20','2024-09-30 06:29:23',0),(7,2,'noreply.dc8framework@gmail.com',NULL,'leban@gmail.com','Email','Outgoing','2024-09-30 06:58:55','Delivered',1,1,'2024-09-30 06:58:55','2024-09-30 06:59:01',0);
/*!40000 ALTER TABLE `Communication` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CommunicationTemplate`
--

DROP TABLE IF EXISTS `CommunicationTemplate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `CommunicationTemplate` (
  `CommunicationTemplateId` int unsigned NOT NULL AUTO_INCREMENT,
  `TemplateName` varchar(255) NOT NULL,
  `Subject` varchar(255) NOT NULL,
  `TemplateContentUrl` varchar(255) NOT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CommunicationTemplateId`),
  UNIQUE KEY `UQ_TemplateName` (`TemplateName`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CommunicationTemplate`
--

LOCK TABLES `CommunicationTemplate` WRITE;
/*!40000 ALTER TABLE `CommunicationTemplate` DISABLE KEYS */;
INSERT INTO `CommunicationTemplate` VALUES (1,'WelcomeEmail','[AppName] Welcome to Our Service!','Assets/MailTemplates/WelcomeEmail.html',NULL,NULL,'2024-09-30 04:35:17',NULL,0),(2,'ResetPassword','[AppName] Reset Your Password','Assets/MailTemplates/ResetPassword.html',NULL,NULL,'2024-09-30 04:35:17',NULL,0),(3,'ResetPasswordSuccess','[AppName] Reset Your Password Successfully!','Assets/MailTemplates/ResetPasswordSuccess.html',NULL,NULL,'2024-09-30 04:35:17',NULL,0),(4,'PromotionEmail','[AppName] Exclusive Offer Just for You!','a',NULL,NULL,'2024-09-30 04:35:17',NULL,0),(5,'MarketingEmail','[AppName] Exclusive Offer Just for You!','b',NULL,NULL,'2024-09-30 04:35:17',NULL,0),(6,'Notification','[AppName] Hey there! You have a new notification!','c',NULL,NULL,'2024-09-30 04:35:17',NULL,0);
/*!40000 ALTER TABLE `CommunicationTemplate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `EmailMetadata`
--

DROP TABLE IF EXISTS `EmailMetadata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `EmailMetadata` (
  `EmailMetadataId` int unsigned NOT NULL AUTO_INCREMENT,
  `CommunicationId` int unsigned NOT NULL,
  `CommunicationTemplateId` int DEFAULT NULL,
  `Subject` varchar(255) DEFAULT NULL,
  `ContentUrl` varchar(255) DEFAULT NULL,
  `ErrorMessage` varchar(500) DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`EmailMetadataId`),
  KEY `CommunicationTemplateId` (`CommunicationTemplateId`),
  KEY `FK_EM_Communication_idx` (`CommunicationId`),
  CONSTRAINT `FK_EM_Communication` FOREIGN KEY (`CommunicationId`) REFERENCES `Communication` (`CommunicationId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `EmailMetadata`
--

LOCK TABLES `EmailMetadata` WRITE;
/*!40000 ALTER TABLE `EmailMetadata` DISABLE KEYS */;
INSERT INTO `EmailMetadata` VALUES (1,1,NULL,'Hello',NULL,NULL,1,NULL,'2024-09-30 04:36:04',NULL,0),(2,2,NULL,'Hello',NULL,NULL,1,NULL,'2024-09-30 04:36:45',NULL,0),(3,3,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:08:17',NULL,0),(4,4,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:11:19',NULL,0),(5,5,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:13:38',NULL,0),(6,6,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:29:20',NULL,0),(7,7,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:58:55',NULL,0);
/*!40000 ALTER TABLE `EmailMetadata` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Permission`
--

DROP TABLE IF EXISTS `Permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Permission` (
  `PermissionId` int unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Code` varchar(50) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`PermissionId`),
  UNIQUE KEY `UQ_Permission_Name_Code` (`Name`,`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Permission`
--

LOCK TABLES `Permission` WRITE;
/*!40000 ALTER TABLE `Permission` DISABLE KEYS */;
INSERT INTO `Permission` VALUES (1,'UserManagement_Read','MGTUSER_R',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(2,'UserManagement_Write','MGTUSER_W',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(3,'RoleManagement_Read','MGTROLE_R',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(4,'RoleManagement_Write','MGTROLE_W',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(5,'PermissionManagement_Read','MGTPER_R',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(6,'PermissionManagement_Write','MGTPER_W',NULL,NULL,NULL,'2024-09-10 09:44:42',NULL,0);
/*!40000 ALTER TABLE `Permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RefreshToken`
--

DROP TABLE IF EXISTS `RefreshToken`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `RefreshToken` (
  `RefreshTokenId` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `Token` varchar(255) NOT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`RefreshTokenId`),
  KEY `FK_RT_UserId` (`UserId`),
  CONSTRAINT `FK_RT_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RefreshToken`
--

LOCK TABLES `RefreshToken` WRITE;
/*!40000 ALTER TABLE `RefreshToken` DISABLE KEYS */;
/*!40000 ALTER TABLE `RefreshToken` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Role`
--

DROP TABLE IF EXISTS `Role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Role` (
  `RoleId` int unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`RoleId`),
  UNIQUE KEY `UQ_Role_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
INSERT INTO `Role` VALUES (1,'System','Responsible for system',NULL,NULL,'2024-04-05 04:50:47',NULL,0),(2,'Admin','Responsible for administrative',NULL,NULL,'2024-04-05 04:50:47',NULL,0),(3,'Client','Responsible for client',NULL,NULL,'2024-04-05 04:50:47',NULL,0),(4,'Test update','Test update desc',1,1,'2024-09-25 04:02:29','2024-09-25 04:04:01',1),(5,'string1234','string',1,1,'2024-09-25 06:40:51','2024-09-25 06:42:02',0),(6,'gdgd','đgdgd',1,NULL,'2024-09-25 07:24:11',NULL,0),(7,'abc','đgdgd',1,NULL,'2024-09-25 07:25:02',NULL,0),(8,'123','ádfff',1,NULL,'2024-09-25 07:26:37',NULL,0),(9,'12344444','ádfff',1,NULL,'2024-09-25 07:30:03',NULL,0),(10,'sdsds','sdsdsđsdsds',1,NULL,'2024-09-25 07:41:07',NULL,0),(11,'sds','sdsd',1,NULL,'2024-09-25 07:41:23',NULL,0),(12,'đ','đ',1,NULL,'2024-09-25 07:54:06',NULL,0),(13,'rr','rr',1,NULL,'2024-09-25 08:19:42',NULL,0),(14,'rrr','rrr',1,NULL,'2024-09-25 08:19:53',NULL,0),(15,'baotrandalen','sdsdsdsds',1,NULL,'2024-09-25 08:49:57',NULL,0),(16,'dd','785555',1,NULL,'2024-09-26 04:40:47',NULL,0),(17,'vcv','vcv',1,NULL,'2024-09-26 08:11:22',NULL,0),(18,'aa','aaaaa',1,NULL,'2024-09-27 03:51:16',NULL,0);
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RolePermission`
--

DROP TABLE IF EXISTS `RolePermission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `RolePermission` (
  `RolePermissionId` int unsigned NOT NULL AUTO_INCREMENT,
  `RoleId` int unsigned DEFAULT NULL,
  `PermissionId` int unsigned DEFAULT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`RolePermissionId`),
  UNIQUE KEY `UQ_Role_Permissions` (`RoleId`,`PermissionId`),
  KEY `FK_RP_PermissionId` (`PermissionId`),
  CONSTRAINT `FK_RP_PermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `Permission` (`PermissionId`),
  CONSTRAINT `FK_RP_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RolePermission`
--

LOCK TABLES `RolePermission` WRITE;
/*!40000 ALTER TABLE `RolePermission` DISABLE KEYS */;
INSERT INTO `RolePermission` VALUES (1,1,1,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(2,1,2,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(3,1,3,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(4,1,4,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(5,1,5,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(6,1,6,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(7,2,3,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(8,2,4,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(9,2,5,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(10,2,6,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(11,3,5,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(12,3,6,NULL,NULL,'2024-09-10 09:47:35',NULL,0);
/*!40000 ALTER TABLE `RolePermission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `User` (
  `UserId` int unsigned NOT NULL AUTO_INCREMENT,
  `RoleId` int unsigned DEFAULT NULL,
  `Username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `FirstName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `LastName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `MobilePhone` varchar(20) DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`UserId`),
  KEY `FK_User_RoleId` (`RoleId`),
  KEY `Idx_User_Username` (`Username`),
  KEY `Idx_User_Email` (`Email`),
  KEY `Idx_User_MobilePhone` (`MobilePhone`),
  CONSTRAINT `FK_User_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (1,1,'admin','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','dc8framework@gmail.com','DC8','Framework','0123456789',NULL,NULL,'2024-04-05 04:50:47',NULL,0),(2,2,'admin1','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','noreply.dc8framework@gmail.com','NoReply','DC8 Framework','0133456789',1,NULL,'2024-04-05 04:50:47',NULL,0),(3,3,'magictree.client','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','client@magictree.com','Client','Test','014567899',1,NULL,'2024-04-05 04:50:47','2024-05-13 10:17:00',0),(48,3,'ngocbich','$2a$10$oIHnTKJDYJWVV/oFVADhiuuB3BR4FhFsaBewsezB4Je8kHd2Cc0TC','dfh@gmail','Bich','Dao','+084377656',1,1,'2024-09-24 04:04:56','2024-09-26 09:28:44',0),(49,3,'ngocbich2','$2a$10$vv/izST.ExXvL9CaEFKz8uat6cwP.5l98X0HTVRMRu1JDz82KHCZG','dffgh@gmail','Bich','Dao','+084377651',1,1,'2024-09-24 09:01:46','2024-09-26 10:53:46',0),(50,3,'ngocbich3','$2a$10$V/zLI3578USwJwHWMDQYhefNJmXIFC1x1H62MdYy7F4jIoZtRfa86','johnn@gmail.com','Bich','Dao','+0378977778',1,1,'2024-09-24 09:25:38','2024-09-25 04:03:28',1),(51,3,'trandoanbaolong','$2a$10$tfSM2giLnt.tMIl6cW1DrufJH1y76KtGLjvCuM09xo/SUJ955JXkO','tranle@gmail.com','long','tran','+84888285128',1,1,'2024-09-24 13:16:16','2024-09-26 09:21:22',0),(52,3,'trandoan','$2a$10$iEgmQzpJ6rz9BwUUCggh3OAPBh86r6j2vKdBZxrH//eckP5m0eDEy','tranbaolong123@gmail.com','long','pham','+84888126871',1,1,'2024-09-24 13:27:37','2024-09-26 07:24:22',0),(53,3,'john','$2a$10$Td40w3I9bqvscy8wvOpSPOX9jUqHhwsU5EYNRohaAGMzsrY6qLDKK','dfghjghh@gmail','john','john','+084377652',1,1,'2024-09-26 08:10:12','2024-09-26 09:18:26',0),(54,3,'johnn','$2a$10$yTu72Jvfya/0ICxVRpRvLeqlNlLHaAbQP6r.fY.kVz/InBmE3.HBi','dfghgfdgjghh@gmail','john','john','+0843776523',1,NULL,'2024-09-26 08:12:53',NULL,0),(55,3,'Thomas','$2a$10$lfOotg6/lAfPJGXPWiWX7O0oOVt3VS1i2Uq9Bd7hYNnGVHB2ozDMG','Thomas@gmail.com','Thomas','Thomas','+0379046333',1,NULL,'2024-09-26 08:47:59',NULL,0),(56,3,'daksuthang0074','$2a$10$yYKR8XnroJSF.I.rMsvLm./tWvUueDoq1aGciIdDzWxyO5YvIHxL6','daksuthang0074@gmail.com','Thang','Huynh','5613213210',1,56,'2024-09-26 10:50:59','2024-09-26 11:14:12',0),(57,3,'anhphan','$2a$10$CAO0jqVta6nopRgV4HXzeOBdLE1kojYgFcw67DgSi41hlhY.5uYuW','tuananh181120@gmail.com','Anh','Phan','+84366671779',1,NULL,'2024-09-27 02:26:06',NULL,0),(58,3,'string','$2a$10$xsvhEWo3sC4WCRkhNPdP7.OU4Uz4BOOvHyDFRGDro7w2t2qB/ZrVC','tranlong@gmail.com','long','le','+84999285145',1,NULL,'2024-09-30 06:08:17',NULL,0),(59,3,'lelong123','$2a$10$tnfHZZV7C0pcSUrbLh/Z0eWeh972Ku/Fj.mproTB4wWz8ADfc11qu','tranlong123@gmail.com','pham','hung','+84952125888',1,NULL,'2024-09-30 06:11:19',NULL,0),(60,3,'congtrinh123','$2a$10$p4uo80YNUjcACN3HfAO5k.CL3FDR5f/u9J8t2zsj1WkzzBgNUcFYu','lehung@gmail.com','long','tran','+84888256941',1,NULL,'2024-09-30 06:13:38',NULL,0),(61,3,'longle','$2a$10$895wfqX0oJUNEAUGLTLu2Oi.7Y5V5bMnE93wPixMPr7cX1wYhuPVW','trantuan@gmail.com','long','ho','+84956125748',1,NULL,'2024-09-30 06:29:20',NULL,0),(62,3,'longle123','$2a$10$oYl3gm59t5An.5akVF3gn..vsJeLhlp5BFfIPCumJkE3YuTJz7AX6','leban@gmail.com','ssss','ssssss','+84888209128',1,NULL,'2024-09-30 06:58:54',NULL,0);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserAccount`
--

DROP TABLE IF EXISTS `UserAccount`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `UserAccount` (
  `UserAccountId` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned DEFAULT NULL,
  `AuthMethod` enum('OAuth','SSO','UsernamePassword','LDAP','SAML','OpenID','JWT','Biometric','MFA','APIKey','Certificate','SocialLogin') NOT NULL,
  `OAuthProvider` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `OAuthProviderUserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `SSOProvider` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `SSOProviderUserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `TwoFactorEnabled` tinyint unsigned DEFAULT '0',
  `TwoFactorSecret` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `TwoFactorBackupCodes` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`UserAccountId`),
  UNIQUE KEY `UQ_UC_Provider` (`UserId`,`AuthMethod`,`OAuthProvider`,`OAuthProviderUserId`),
  CONSTRAINT `FK_UserAccount_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserAccount`
--

LOCK TABLES `UserAccount` WRITE;
/*!40000 ALTER TABLE `UserAccount` DISABLE KEYS */;
INSERT INTO `UserAccount` VALUES (1,1,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-07-08 10:56:06',NULL,0),(25,48,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 04:04:56',NULL,0),(26,49,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 09:01:47',NULL,0),(27,50,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 09:25:38',NULL,0),(28,51,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 13:16:16',NULL,0),(29,52,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 13:27:37',NULL,0),(30,53,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:10:12',NULL,0),(31,54,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:12:53',NULL,0),(32,55,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:47:59',NULL,0),(33,56,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 10:50:59',NULL,0),(34,57,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-27 02:26:06',NULL,0),(35,58,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:08:17',NULL,0),(36,59,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:11:19',NULL,0),(37,60,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:13:38',NULL,0),(38,61,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:29:20',NULL,0),(39,62,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:58:55',NULL,0);
/*!40000 ALTER TABLE `UserAccount` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-30 14:14:03
