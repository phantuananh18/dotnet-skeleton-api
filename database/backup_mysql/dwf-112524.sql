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
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Communication`
--

LOCK TABLES `Communication` WRITE;
/*!40000 ALTER TABLE `Communication` DISABLE KEYS */;
INSERT INTO `Communication` VALUES (1,1,'dc8framework@gmail.com',NULL,'daksuthang0074@gmail.com','Email','Outgoing','2024-09-30 04:36:04','Delivered',1,1,'2024-09-30 04:36:04','2024-09-30 04:36:08',0),(2,1,'dc8framework@gmail.com',NULL,'daksuthang0074@gmail.com','Email','Outgoing','2024-09-30 04:36:44','Delivered',1,1,'2024-09-30 04:36:45','2024-09-30 04:36:48',0),(3,2,'noreply.dc8framework@gmail.com',NULL,'tranlong@gmail.com','Email','Outgoing','2024-09-30 06:08:17','Delivered',1,1,'2024-09-30 06:08:17','2024-09-30 06:08:20',0),(4,2,'noreply.dc8framework@gmail.com',NULL,'tranlong123@gmail.com','Email','Outgoing','2024-09-30 06:11:19','Delivered',1,1,'2024-09-30 06:11:19','2024-09-30 06:11:22',0),(5,2,'noreply.dc8framework@gmail.com',NULL,'lehung@gmail.com','Email','Outgoing','2024-09-30 06:13:38','Delivered',1,1,'2024-09-30 06:13:38','2024-09-30 06:13:42',0),(6,2,'noreply.dc8framework@gmail.com',NULL,'trantuan@gmail.com','Email','Outgoing','2024-09-30 06:29:20','Delivered',1,1,'2024-09-30 06:29:20','2024-09-30 06:29:23',0),(7,2,'noreply.dc8framework@gmail.com',NULL,'leban@gmail.com','Email','Outgoing','2024-09-30 06:58:55','Delivered',1,1,'2024-09-30 06:58:55','2024-09-30 06:59:01',0),(8,2,'noreply.dc8framework@gmail.com',NULL,'hovanlam@gmail.com','Email','Outgoing','2024-09-30 07:19:59','Delivered',1,1,'2024-09-30 07:19:59','2024-09-30 07:20:02',0),(9,2,'noreply.dc8framework@gmail.com',NULL,'hellodc8@gmail.com','Email','Outgoing','2024-09-30 08:03:39','Delivered',1,1,'2024-09-30 08:03:40','2024-09-30 08:03:43',0),(10,2,'noreply.dc8framework@gmail.com',NULL,'tquangtuan1234@tma.com.vn','Email','Outgoing','2024-09-30 08:12:05','Delivered',1,1,'2024-09-30 08:12:05','2024-09-30 08:12:08',0),(11,1,'dc8framework@gmail.com',NULL,'thanghuynh26081@gmail.com','Email','Outgoing','2024-09-30 08:33:07','Delivered',1,1,'2024-09-30 08:33:07','2024-09-30 08:33:12',0),(12,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-04 08:53:00','Delivered',1,1,'2024-10-04 08:53:00','2024-10-04 08:53:27',0),(13,2,'noreply.dc8framework@gmail.com',NULL,'yoralong1@gmail.com','Email','Outgoing','2024-10-21 10:48:58','Failed',1,1,'2024-10-21 10:48:58','2024-10-21 10:49:07',0),(14,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-21 10:55:33','Pending',1,NULL,'2024-10-21 10:55:33',NULL,0),(15,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-21 11:02:59','Delivered',1,1,'2024-10-21 11:02:59','2024-10-21 11:03:18',0),(16,2,'noreply.dc8framework@gmail.com',NULL,'yoralong1@gmail.com','Email','Outgoing','2024-10-22 09:29:01','Delivered',1,1,'2024-10-22 09:29:01','2024-10-22 09:29:13',0),(17,2,'noreply.dc8framework@gmail.com',NULL,'yoralong1@gmail.com','Email','Outgoing','2024-10-22 09:31:51','Delivered',1,1,'2024-10-22 09:31:51','2024-10-22 09:31:55',0),(18,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 09:39:11','Delivered',1,1,'2024-10-22 09:39:11','2024-10-22 09:42:45',0),(19,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 09:42:55','Delivered',1,1,'2024-10-22 09:42:55','2024-10-22 09:43:00',0),(20,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 09:44:44','Pending',1,NULL,'2024-10-22 09:44:44',NULL,0),(21,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 09:56:40','Delivered',1,1,'2024-10-22 09:56:40','2024-10-22 10:00:15',0),(22,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 10:09:14','Delivered',1,1,'2024-10-22 10:09:14','2024-10-22 10:09:32',0),(23,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-10-22 10:11:27','Delivered',1,1,'2024-10-22 10:11:27','2024-10-22 10:11:35',0),(24,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-07 07:38:04','Delivered',1,1,'2024-11-07 07:38:04','2024-11-07 07:38:21',0),(25,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-07 07:41:15','Delivered',1,1,'2024-11-07 07:41:15','2024-11-07 07:41:21',0),(26,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-13 08:06:31','Delivered',1,1,'2024-11-13 08:06:31','2024-11-13 08:11:25',0),(27,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-13 08:11:52','Delivered',1,1,'2024-11-13 08:11:52','2024-11-13 08:11:55',0),(28,2,'noreply.dc8framework@gmail.com',NULL,'pippette@chupanhcuoidep.vn','Email','Outgoing','2024-11-13 09:43:03','Delivered',1,1,'2024-11-13 09:43:03','2024-11-13 09:43:06',0),(29,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-13 09:45:21','Delivered',1,1,'2024-11-13 09:45:21','2024-11-13 09:45:24',0),(30,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-13 10:13:53','Delivered',1,1,'2024-11-13 10:13:53','2024-11-13 10:14:12',0),(31,2,'noreply.dc8framework@gmail.com',NULL,'yoralong@gmail.com','Email','Outgoing','2024-11-21 07:13:24','Delivered',1,1,'2024-11-21 07:13:24','2024-11-21 07:13:28',0),(32,2,'noreply.dc8framework@gmail.com',NULL,'sviryi@gmailbrt.com','Email','Outgoing','2024-11-22 04:39:54','Delivered',1,1,'2024-11-22 04:39:54','2024-11-22 04:39:58',0);
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
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `EmailMetadata`
--

LOCK TABLES `EmailMetadata` WRITE;
/*!40000 ALTER TABLE `EmailMetadata` DISABLE KEYS */;
INSERT INTO `EmailMetadata` VALUES (1,1,NULL,'Hello',NULL,NULL,1,NULL,'2024-09-30 04:36:04',NULL,0),(2,2,NULL,'Hello',NULL,NULL,1,NULL,'2024-09-30 04:36:45',NULL,0),(3,3,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:08:17',NULL,0),(4,4,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:11:19',NULL,0),(5,5,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:13:38',NULL,0),(6,6,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:29:20',NULL,0),(7,7,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 06:58:55',NULL,0),(8,8,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 07:19:59',NULL,0),(9,9,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 08:03:40',NULL,0),(10,10,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-09-30 08:12:05',NULL,0),(11,11,NULL,'Hello',NULL,NULL,1,NULL,'2024-09-30 08:33:09',NULL,0),(12,12,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-10-04 08:53:00',NULL,0),(13,13,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-10-21 10:48:59',NULL,0),(14,14,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-21 10:55:34',NULL,0),(15,15,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-21 11:02:59',NULL,0),(16,16,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-10-22 09:29:03',NULL,0),(17,17,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 09:31:51',NULL,0),(18,18,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 09:39:11',NULL,0),(19,19,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 09:42:55',NULL,0),(20,20,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 09:44:44',NULL,0),(21,21,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 09:56:40',NULL,0),(22,22,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-10-22 10:09:14',NULL,0),(23,23,3,'[AppName] Reset Your Password Successfully!',NULL,NULL,1,NULL,'2024-10-22 10:11:27',NULL,0),(24,24,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-07 07:38:06',NULL,0),(25,25,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-07 07:41:16',NULL,0),(26,26,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-13 08:06:35',NULL,0),(27,27,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-13 08:11:52',NULL,0),(28,28,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-11-13 09:43:03',NULL,0),(29,29,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-13 09:45:21',NULL,0),(30,30,3,'[AppName] Reset Your Password Successfully!',NULL,NULL,1,NULL,'2024-11-13 10:13:54',NULL,0),(31,31,2,'[AppName] Reset Your Password',NULL,NULL,1,NULL,'2024-11-21 07:13:24',NULL,0),(32,32,1,'[AppName] Welcome to Our Service!',NULL,NULL,1,NULL,'2024-11-22 04:39:55',NULL,0);
/*!40000 ALTER TABLE `EmailMetadata` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Feature`
--

DROP TABLE IF EXISTS `Feature`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Feature` (
  `FeatureId` int unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`FeatureId`),
  UNIQUE KEY `UQ_Permission_Name_Code` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Feature`
--

LOCK TABLES `Feature` WRITE;
/*!40000 ALTER TABLE `Feature` DISABLE KEYS */;
INSERT INTO `Feature` VALUES (1,'User Management','User Management',1,NULL,'2024-10-01 10:46:06',NULL,0),(2,'Role Management','Role Management',1,NULL,'2024-10-01 10:46:06',NULL,0),(3,'Permission Management','Permission Management',1,NULL,'2024-10-01 10:46:06',NULL,0);
/*!40000 ALTER TABLE `Feature` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Notification`
--

DROP TABLE IF EXISTS `Notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Notification` (
  `NotificationId` bigint unsigned NOT NULL AUTO_INCREMENT,
  `NotificationTypeId` int unsigned NOT NULL,
  `TriggeredUserId` int unsigned DEFAULT NULL,
  `Title` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `SenderInfo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`NotificationId`),
  KEY `FK_Notification_NotificationTypeId` (`NotificationTypeId`),
  KEY `FK_Notification_TriggeredUserId` (`TriggeredUserId`),
  CONSTRAINT `FK_Notification_NotificationTypeId` FOREIGN KEY (`NotificationTypeId`) REFERENCES `NotificationType` (`NotificationTypeId`),
  CONSTRAINT `FK_Notification_TiggeredUserId` FOREIGN KEY (`TriggeredUserId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Notification`
--

LOCK TABLES `Notification` WRITE;
/*!40000 ALTER TABLE `Notification` DISABLE KEYS */;
INSERT INTO `Notification` VALUES (8,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',68,68,'2024-10-04 10:08:36','2024-10-04 10:08:36',0),(9,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-04 10:17:10','2024-10-04 10:17:10',0),(10,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-07 08:27:39','2024-10-07 08:27:39',0),(11,1,NULL,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-08 10:54:02','2024-10-08 10:54:02',0),(12,1,NULL,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-08 11:21:23','2024-10-08 11:21:23',0),(13,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-08 11:29:11','2024-10-08 11:29:11',0),(14,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-08 11:31:33','2024-10-08 11:31:33',0),(15,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-08 11:43:24','2024-10-08 11:43:24',0),(16,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-09 02:49:57','2024-10-09 02:49:57',0),(17,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',68,68,'2024-10-09 03:46:18','2024-10-09 03:46:18',0),(18,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',68,68,'2024-10-09 03:51:49','2024-10-09 03:51:49',0),(19,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com\'',68,68,'2024-10-09 04:14:26','2024-10-09 04:14:26',0),(20,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',1,1,'2024-10-09 11:14:18','2024-10-09 11:14:18',0),(21,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',68,68,'2024-10-09 11:25:28','2024-10-09 11:25:28',0),(22,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',1,1,'2024-10-09 11:25:42','2024-10-09 11:25:42',0),(23,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',1,1,'2024-10-10 06:59:42','2024-10-10 06:59:42',0),(24,1,53,'New user have been created','New user have been created in your system','dc8framework@gmail.com',1,1,'2024-10-10 07:02:40','2024-10-10 07:02:40',0),(25,1,53,'Add New User','Hello bro','dc8framework@gmail.com',68,68,'2024-10-18 03:35:07','2024-10-18 03:35:07',0),(26,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',68,68,'2024-10-18 03:40:34','2024-10-18 03:40:34',0),(27,2,53,'You have new Email','Hello bro, you have new Email','dc8framework@gmail.com',68,68,'2024-10-18 10:24:09','2024-10-18 10:24:09',0);
/*!40000 ALTER TABLE `Notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NotificationRecipient`
--

DROP TABLE IF EXISTS `NotificationRecipient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `NotificationRecipient` (
  `NotificationRecipientId` bigint unsigned NOT NULL AUTO_INCREMENT,
  `RecipientId` int unsigned NOT NULL,
  `NotificationId` bigint unsigned NOT NULL,
  `IsRead` tinyint unsigned NOT NULL DEFAULT '0',
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`NotificationRecipientId`),
  KEY `FK_NotificationRecipient_RecipientId` (`RecipientId`),
  KEY `FK_NotificationRecipient_NotificationId` (`NotificationId`),
  CONSTRAINT `FK_NotificationRecipient_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notification` (`NotificationId`),
  CONSTRAINT `FK_NotificationRecipient_RecipientId` FOREIGN KEY (`RecipientId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NotificationRecipient`
--

LOCK TABLES `NotificationRecipient` WRITE;
/*!40000 ALTER TABLE `NotificationRecipient` DISABLE KEYS */;
INSERT INTO `NotificationRecipient` VALUES (1,53,8,0,68,68,'2024-10-04 10:08:36','2024-10-04 10:08:36',0),(2,2,9,0,68,68,'2024-10-04 10:17:10','2024-10-04 10:17:10',0),(3,68,9,0,68,68,'2024-10-04 10:17:10','2024-10-04 10:17:10',0),(4,1,9,0,68,68,'2024-10-04 10:17:10','2024-10-04 10:17:10',0),(5,2,10,0,68,68,'2024-10-07 08:27:39','2024-10-07 08:27:39',0),(6,68,10,0,68,68,'2024-10-07 08:27:39','2024-10-07 08:27:39',0),(7,1,10,0,68,68,'2024-10-07 08:27:39','2024-10-07 08:27:39',0),(8,2,11,0,68,68,'2024-10-08 10:54:02','2024-10-08 10:54:02',0),(9,68,11,0,68,68,'2024-10-08 10:54:02','2024-10-08 10:54:02',0),(11,2,12,0,68,68,'2024-10-08 11:21:23','2024-10-08 11:21:23',0),(12,68,12,0,68,68,'2024-10-08 11:21:23','2024-10-08 11:21:23',0),(14,2,13,0,68,68,'2024-10-08 11:29:11','2024-10-08 11:29:11',0),(15,68,13,0,68,68,'2024-10-08 11:29:11','2024-10-08 11:29:11',0),(17,2,14,0,68,68,'2024-10-08 11:31:33','2024-10-08 11:31:33',0),(18,68,14,0,68,68,'2024-10-08 11:31:33','2024-10-08 11:31:33',0),(20,2,15,0,68,68,'2024-10-08 11:43:24','2024-10-08 11:43:24',0),(21,68,15,0,68,68,'2024-10-08 11:43:24','2024-10-08 11:43:24',0),(23,2,16,0,68,68,'2024-10-09 02:49:57','2024-10-09 02:49:57',0),(24,68,16,0,68,68,'2024-10-09 02:49:57','2024-10-09 02:49:57',0),(26,2,17,0,68,68,'2024-10-09 03:46:18','2024-10-09 03:46:18',0),(27,68,17,0,68,68,'2024-10-09 03:46:18','2024-10-09 03:46:18',0),(29,53,18,0,68,68,'2024-10-09 03:51:49','2024-10-09 03:51:49',0),(30,53,19,0,68,68,'2024-10-09 04:14:26','2024-10-09 04:14:26',0),(31,53,20,0,1,1,'2024-10-09 11:14:18','2024-10-09 11:14:18',0),(32,53,21,0,68,68,'2024-10-09 11:25:28','2024-10-09 11:25:28',0),(33,53,22,0,1,1,'2024-10-09 11:25:42','2024-10-09 11:25:42',0),(34,53,23,0,1,1,'2024-10-10 06:59:42','2024-10-10 06:59:42',0),(35,2,24,0,1,1,'2024-10-10 07:02:40','2024-10-10 07:02:40',0),(36,68,24,0,1,1,'2024-10-10 07:02:40','2024-10-10 07:02:40',0),(38,2,25,0,68,68,'2024-10-18 03:35:07','2024-10-18 03:35:07',0),(39,68,25,0,68,68,'2024-10-18 03:35:07','2024-10-18 03:35:07',0),(41,53,26,0,68,68,'2024-10-18 03:40:34','2024-10-18 03:40:34',0),(42,53,27,0,68,68,'2024-10-18 10:24:09','2024-10-18 10:24:09',0);
/*!40000 ALTER TABLE `NotificationRecipient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NotificationType`
--

DROP TABLE IF EXISTS `NotificationType`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `NotificationType` (
  `NotificationTypeId` int unsigned NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`NotificationTypeId`),
  UNIQUE KEY `UQ_NotificationType_TypeName` (`TypeName`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NotificationType`
--

LOCK TABLES `NotificationType` WRITE;
/*!40000 ALTER TABLE `NotificationType` DISABLE KEYS */;
INSERT INTO `NotificationType` VALUES (1,'New User',NULL,1,1,'2024-10-01 10:25:47','2024-10-01 10:25:47',0),(2,'New Email',NULL,1,1,'2024-10-01 10:25:47','2024-10-01 10:25:47',0),(3,'New Sms',NULL,1,1,'2024-10-01 10:25:47','2024-10-01 10:25:47',0);
/*!40000 ALTER TABLE `NotificationType` ENABLE KEYS */;
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
  `FeatureId` int unsigned NOT NULL,
  `CreatedBy` int unsigned DEFAULT NULL,
  `UpdatedBy` int unsigned DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`PermissionId`),
  UNIQUE KEY `UQ_Permission_Name_Code` (`Name`,`Code`),
  UNIQUE KEY `UQ_Permission_Feature` (`Code`,`FeatureId`),
  KEY `FK_Permission_FeatureId_idx` (`FeatureId`),
  CONSTRAINT `FK_Permission_FeatureId` FOREIGN KEY (`FeatureId`) REFERENCES `Feature` (`FeatureId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Permission`
--

LOCK TABLES `Permission` WRITE;
/*!40000 ALTER TABLE `Permission` DISABLE KEYS */;
INSERT INTO `Permission` VALUES (1,'UserManagement_Read','MGTUSER_R',NULL,1,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(2,'UserManagement_Write','MGTUSER_W',NULL,1,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(3,'RoleManagement_Read','MGTROLE_R',NULL,2,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(4,'RoleManagement_Write','MGTROLE_W',NULL,2,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(5,'PermissionManagement_Read','MGTPER_R',NULL,3,NULL,NULL,'2024-09-10 09:44:42',NULL,0),(6,'PermissionManagement_Write','MGTPER_W','string',3,NULL,1,'2024-09-10 09:44:42','2024-11-25 03:51:54',0);
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
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
INSERT INTO `Role` VALUES (1,'System','Responsible for system',NULL,48,'2024-04-05 04:50:47','2024-11-11 09:15:40',0),(2,'Admin','Responsible for administrative',NULL,48,'2024-04-05 04:50:47','2024-11-19 04:01:56',0),(3,'Client','Responsible for client',NULL,48,'2024-04-05 04:50:47','2024-11-11 09:30:52',0),(29,'Test Role 1','Test Role',1,1,'2024-11-22 08:54:14','2024-11-22 09:03:35',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RolePermission`
--

LOCK TABLES `RolePermission` WRITE;
/*!40000 ALTER TABLE `RolePermission` DISABLE KEYS */;
INSERT INTO `RolePermission` VALUES (1,1,1,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(2,1,2,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(3,1,3,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(4,1,4,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(5,1,5,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(6,1,6,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(7,2,3,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(8,2,4,NULL,48,'2024-09-10 09:47:35','2024-11-18 10:55:45',0),(9,2,5,NULL,48,'2024-09-10 09:47:35','2024-11-18 03:16:46',0),(10,2,6,NULL,48,'2024-09-10 09:47:35','2024-11-11 10:07:25',0),(11,3,5,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(12,3,6,NULL,NULL,'2024-09-10 09:47:35',NULL,0),(14,3,4,1,NULL,'2024-11-01 11:10:29',NULL,0),(16,3,3,1,1,'2024-11-04 07:53:14','2024-11-04 07:56:00',1),(17,2,2,48,48,'2024-11-11 10:09:18','2024-11-11 10:09:20',1),(18,2,1,1,NULL,'2024-11-22 09:01:47',NULL,0);
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
  `ProfilePictureUrl` varchar(255) DEFAULT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (1,1,'admin','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','dc8framework@gmail.com','DC8','Framework','0123456789',NULL,NULL,NULL,'2024-04-05 04:50:47',NULL,0),(2,2,'admin1','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','noreply.dc8framework@gmail.com','NoReply','DC8 Framework','0133456789',NULL,1,NULL,'2024-04-05 04:50:47',NULL,0),(3,3,'magictree.client','$2b$10$/FiSFuwCR0PzR3rMZL9Y6uLFEW3bi29Wvu5FYlcLVrT7vgxxcciNy','client@magictree.com','Client','Test','+0145678999',NULL,1,1,'2024-04-05 04:50:47','2024-09-30 10:38:55',0),(48,2,'ngocbich','$2a$10$oIHnTKJDYJWVV/oFVADhiuuB3BR4FhFsaBewsezB4Je8kHd2Cc0TC','dfh@gmail','PleaseDoNotDeleteThis','Dao','+0843776567',NULL,1,95,'2024-09-24 04:04:56','2024-11-12 07:38:21',0),(49,2,'ngocbich2','$2a$10$vv/izST.ExXvL9CaEFKz8uat6cwP.5l98X0HTVRMRu1JDz82KHCZG','dffgh@gmail','Bich','Dao','+084377651',NULL,1,95,'2024-09-24 09:01:46','2024-11-12 08:24:13',0),(50,3,'ngocbich3','$2a$10$V/zLI3578USwJwHWMDQYhefNJmXIFC1x1H62MdYy7F4jIoZtRfa86','johnn@gmail.com','Bich','Dao','+0378977778',NULL,1,1,'2024-09-24 09:25:38','2024-09-25 04:03:28',1),(51,3,'trandoanbaolong','$2a$10$tfSM2giLnt.tMIl6cW1DrufJH1y76KtGLjvCuM09xo/SUJ955JXkO','tranle@gmail.com','long','tran','+84888285128',NULL,1,1,'2024-09-24 13:16:16','2024-09-26 09:21:22',0),(52,3,'trandoan','$2a$10$iEgmQzpJ6rz9BwUUCggh3OAPBh86r6j2vKdBZxrH//eckP5m0eDEy','tranbaolong123@gmail.com','long','pham','+84888126871',NULL,1,1,'2024-09-24 13:27:37','2024-09-26 07:24:22',0),(53,3,'john','$2a$10$Td40w3I9bqvscy8wvOpSPOX9jUqHhwsU5EYNRohaAGMzsrY6qLDKK','dfghjghh@gmail','john','john','+084377652',NULL,1,1,'2024-09-26 08:10:12','2024-09-26 09:18:26',0),(54,3,'johnn','$2a$10$yTu72Jvfya/0ICxVRpRvLeqlNlLHaAbQP6r.fY.kVz/InBmE3.HBi','dfghgfdgjghh@gmail','john','johnn','+0843776523',NULL,1,1,'2024-09-26 08:12:53','2024-09-30 08:17:54',0),(55,3,'Thomas','$2a$10$lfOotg6/lAfPJGXPWiWX7O0oOVt3VS1i2Uq9Bd7hYNnGVHB2ozDMG','Thomas@gmail.com','Thomas','Thomas','+0379046333',NULL,1,NULL,'2024-09-26 08:47:59',NULL,0),(56,3,'daksuthang0074','$2a$10$yYKR8XnroJSF.I.rMsvLm./tWvUueDoq1aGciIdDzWxyO5YvIHxL6','daksuthang0074@gmail.com','Thang','Huynh','5613213210',NULL,1,56,'2024-09-26 10:50:59','2024-09-26 11:14:12',0),(57,3,'anhphan','$2a$10$CAO0jqVta6nopRgV4HXzeOBdLE1kojYgFcw67DgSi41hlhY.5uYuW','tuananh181120@gmail.com','Anh','Phan','+843666717799',NULL,1,48,'2024-09-27 02:26:06','2024-11-18 03:12:36',0),(58,3,'string','$2a$10$xsvhEWo3sC4WCRkhNPdP7.OU4Uz4BOOvHyDFRGDro7w2t2qB/ZrVC','tranlong@gmail.com','long','le','+84999285145',NULL,1,NULL,'2024-09-30 06:08:17',NULL,0),(59,3,'lelong123','$2a$10$tnfHZZV7C0pcSUrbLh/Z0eWeh972Ku/Fj.mproTB4wWz8ADfc11qu','tranlong123@gmail.com','pham','hung','+84952125888',NULL,1,NULL,'2024-09-30 06:11:19',NULL,0),(60,3,'congtrinh123','$2a$10$p4uo80YNUjcACN3HfAO5k.CL3FDR5f/u9J8t2zsj1WkzzBgNUcFYu','lehung@gmail.com','long','tran','+84888256941',NULL,1,NULL,'2024-09-30 06:13:38',NULL,0),(61,3,'longle','$2a$10$895wfqX0oJUNEAUGLTLu2Oi.7Y5V5bMnE93wPixMPr7cX1wYhuPVW','trantuan@gmail.com','long','ho','+84956125748',NULL,1,NULL,'2024-09-30 06:29:20',NULL,0),(62,3,'longle123','$2a$10$oYl3gm59t5An.5akVF3gn..vsJeLhlp5BFfIPCumJkE3YuTJz7AX6','leban@gmail.com','ssss','ssssss','+84888209128',NULL,1,NULL,'2024-09-30 06:58:54',NULL,0),(63,3,'hovanlam','$2a$10$JWR5rtMQvSw4XbLFzwOlg.Ihbcu7FlDq6ZWgYZbeSxEyP9Kaa2EXe','hovanlam@gmail.com','ho','lam','+84888359412',NULL,1,NULL,'2024-09-30 07:19:59',NULL,0),(64,3,'tquangtuan','$2a$10$tGVxiEU5HD4bRQUWn92pDuAIrHWO3IbTTPurj78TTifAo6DNG0KlK','tquangtuan@tma.com.vn','Tuan','Tran','+15055555555',NULL,1,NULL,'2024-09-30 08:02:31',NULL,0),(65,3,'hellodc8','$2a$10$pEmu86zqz3rYd7FtzvC7yO3tDFUgI7xp6E6Jxi8OaNKDV5gJ.wdzm','hellodc8@gmail.com','hello','dc8','+8456132131',NULL,1,NULL,'2024-09-30 08:03:39',NULL,0),(66,3,'tquangtuan1234','$2a$10$3Tsp3D5rWQNwWAJXBzU33uwrQqczAYoDGFfHrmHoXlHgyk8musoXa','tquangtuan1234@tma.com.vn','Tuan','Tran','+15055555569',NULL,1,1,'2024-09-30 08:12:05','2024-09-30 08:13:44',0),(67,3,'ngocbich4','$2a$10$7mCEG3O6YPYLpO4vqipOK./Vv/NcPFP5U87fuCaGykT/uytD76jUG','bich1234@gmail.com','Bich','Dao','+0843776524',NULL,1,1,'2024-10-01 08:54:00','2024-10-01 09:31:49',1),(68,2,'yoralong','$2a$10$HgohbSYzsN80h7yj3uTsY.J0gqcSTuLlelCUIKpq4f..KSb.ws.X6','yoralong@gmail.com','Yora','Long','84336385941',NULL,1,1,'2024-10-04 08:53:00','2024-11-13 10:13:46',0),(71,2,'yoralong1','$2a$10$4/o0AsFoRBRS3dIAy0h9x.rJcsZUM9hCF2dfye1TbdaniETRVQvO2','yoralong1@gmail.com','Yora','Long','0336385942',NULL,1,NULL,'2024-10-22 09:28:49',NULL,0),(72,3,'thinh','$2a$10$/GGXmSd39h/f9LO4LEwwx.Qw9WzdFI19rYpieIu/Dd4RUlC3ZtC0e','test@tma.com.vn','Thinh','Phan','+123456789',NULL,48,48,'2024-10-25 03:16:25','2024-11-06 02:26:43',1),(73,3,'newoauth','$2a$10$oIcx6pH0u317XXF1LaRuheykp3L3ZABB.LPAAT9430/gTYmjUPWX2','newoauth@abc.com','New','Acciybt','+561321345',NULL,1,NULL,'2024-10-31 04:46:19',NULL,0),(74,3,'newoa1uth','$2a$10$YkJe3N9F8hgEjrZaB/97oe7rdE9HsmQkDL4rUEy0aXj0dmsso81Bm','newoa1uth@abc.com','New4','Acciybt','+561321385',NULL,1,NULL,'2024-10-31 07:04:57',NULL,0),(75,3,'newoa1u12th','$2a$10$ix99zrzjwkzP2sJTtqsqweS5E6FFw1zsyJccTeirLs/AUY7Je3APa','newoa1u12th@abc.com','New44','Acciybt1','+561321381',NULL,1,NULL,'2024-10-31 07:06:45',NULL,0),(76,3,'newoa1u13th','$2a$10$xvY5Ry3DPMd9OiVwnGeReeSci0lXJuhEkuzTx5sFl4hGJkCsfR8PK','newoa1u13th@abc.com','New43','Acciybt3','+561321382',NULL,1,NULL,'2024-10-31 07:08:36',NULL,0),(77,3,'newoa1u14th','$2a$10$TzM24at6nEN9W3jgeMyjY.DxJy02P7V95nGdGEjy8eTY3B6WxCGQ6','newoa1u14th@abc.com','New44','Acciybt3','+561321383',NULL,1,NULL,'2024-10-31 07:13:54',NULL,0),(78,3,'newoa1u16th','$2a$10$YbzNSDyEPziOXpirS7viB.ShgE6906CXwf2BMOdhdKobQ6HlWxsaa','newoa1u16th@abc.com','New46','Acciybt3','+561321386',NULL,1,NULL,'2024-10-31 07:18:15',NULL,0),(79,3,'newoa1u17th','$2a$10$PGup9O/eLj1lJDRov8JStecOSEFlpr2YYHcwvJoQgtbx7tkhLfLv6','newoa1u17th@abc.com','New47','Acciybt3','+561321387',NULL,1,NULL,'2024-10-31 07:21:51',NULL,0),(80,3,'newoa1u18th','$2a$10$uSE48dBPN5zGZ.pAwyc16uh2mMtXHA6xyMhjE0MN9ipdVc3dxBMPy','newoa1u18th@abc.com','New48','Acciybt3','+561321388',NULL,1,NULL,'2024-10-31 07:29:15',NULL,0),(81,3,'newoa1u19th','$2a$10$GNi2j.lV5wG5jCH0k0aIuOC9ArUTd00bsK7Hh1OfYGvuNLGhePZHW','newoa1u19th@abc.com','New49','Acciybt3','+561321389',NULL,1,NULL,'2024-10-31 07:31:20',NULL,0),(82,3,'newoa1u20th','$2a$10$qUb6iV5zkg2sR29x58sxp.1ftiVzPXwyD2sfNFu9Dm.0/GTJdv9wK','newoa1u20th@abc.com','New50','Acciybt3','+561321390',NULL,1,NULL,'2024-10-31 07:31:53',NULL,0),(83,3,'newoa1u21th','$2a$10$p3syV3j6wGaBY.L40BvemO0ouP4nIxBWpImuZEPYDF0I5O8ziSCsC','newoa1u21th@abc.com','New51','Acciybt3','+561321391',NULL,1,NULL,'2024-10-31 07:35:35',NULL,0),(84,3,'newoa1u22th','$2a$10$xIsOFMSj4OdaaJ6jMkkoVuiP09YAZRylxhAcWW9gWuLpGWrSECkwS','newoa1u22th@abc.com','New52','Acciybt3','+561321392',NULL,1,NULL,'2024-10-31 07:40:22',NULL,0),(85,3,'newoa1u23th','$2a$10$6sPvEeTaNgbxPI.EFtKINOQoIPfn8SimaTSQBcAi33ErUYv/fU1LK','newoa1u23th@abc.com','New53','Acciybt3','+561321393',NULL,1,NULL,'2024-10-31 07:45:43',NULL,0),(86,3,'newoa1u24th','$2a$10$OrAR8C0jP4qDUVFU5Ebk8Opt1.ZNecUngaYYivrS3c8PoliB0EHiW','newoa1u24th@abc.com','New54','Acciybt3','+561321394',NULL,1,NULL,'2024-10-31 07:47:35',NULL,0),(87,3,'newoa1u25th','$2a$10$X/4Z/85NCgdmyBCMZTtdwutZNeKSo.grIELMaHIXgqatSQOT.MrMq','newoa1u25th@abc.com','New55','Acciybt3','+561321395',NULL,1,NULL,'2024-10-31 08:06:43',NULL,0),(88,3,'newoa1u26th','$2a$10$t9LkLqjfrInwH8cDncgPQ.qtLH2fVmAlUAJR4oonUEijtT/vrv44C','newoa1u26th@abc.com','New56','Acciybt3','+561321396',NULL,1,NULL,'2024-10-31 08:07:17',NULL,0),(89,3,'newoa1u27th','$2a$10$MfO82tCi50NMj11A2JO9UO90RXsdPYzJmIMkUzRVoL/JU3RC5m9dC','newoa1u27th@abc.com','New57','Acciybt3','+561321397',NULL,1,NULL,'2024-10-31 08:08:10',NULL,0),(90,3,'newoa1u28th','$2a$10$alPmc.7Rpr0b/nkuicOcIOFJIfVbrhIBlOHOObIcSnZxupsIMc50O','newoa1u28th@abc.com','New58','Acciybt3','+561321398',NULL,1,NULL,'2024-10-31 08:10:52',NULL,0),(91,3,'newoa1u29th','$2a$10$WCUh9Vjzwr59e9djg1b2bOouLVtLuwlE2g2r2Us.GFVXmgf.SMRPu','newoa1u29th@abc.com','New59','Acciybt3','+561321399',NULL,1,NULL,'2024-10-31 08:13:00',NULL,0),(92,3,'newoa1u30h','$2a$10$/tJOu32YJF7xJ2WSWNNZ4.72LeyeH/ddsDeKteiHykjj5WmEfcHCm','newoa1u30h@abc.com','Change Name','Name','+5613213400',NULL,1,1,'2024-11-01 09:13:49','2024-11-13 10:24:16',0),(93,3,'newoa1u31h','$2a$10$PEoZGDFIb.jUzHUUESL8D.sYvkhsdUDZj2NzkYIzT6phEgBcNyxDW','newoa1u31h@abc.com','Change Abc','Xyz','+5613213401',NULL,1,1,'2024-11-06 08:31:45','2024-11-06 08:34:53',0),(94,3,'bichdao','$2a$10$Bt.8GsQw0k5odbbUUjirAevJV1n/E9Czl6bj7xAcptXyPbUGDmRFW','bichdao@gmail.com','bichdao','bichdao','+86759071','https://avatars.githubusercontent.com/u/86759071?v=4',1,95,'2024-11-11 11:02:15','2024-11-12 07:05:38',0),(95,3,'bichdao090202','$2a$10$bK7haSlauZPxu0oVes9J3e3bigQe7ciUndfuwvHgIdhuSh1YSdEHW','bichdao090202@gmail.com','Dao Thi Ngoc Bich','','86759071',NULL,1,1,'2024-11-12 05:08:08','2024-11-19 04:01:24',0),(96,2,'hunter98','$2a$10$2lb7PZXpI5ikl8FQffdB.e28lDuqx5F5ef1UOTgmBhfNPBIGymRQy','hunter98@pemberontakjaya88.com','Yora','hunter','0336385943',NULL,1,NULL,'2024-11-12 08:17:32',NULL,0),(97,2,'hunter989','$2a$10$LzBaZ36OEeJYl3PRrC/.GeoUg/jzpfD.ZC8O9LZ5cujxVEWeBuTEG','kolya2bragarnik@naverly.com','kolya2bragarnik','hunter','0336385946',NULL,1,NULL,'2024-11-12 10:52:05',NULL,0),(98,2,'pippette','$2a$10$1P36G36U8fR86DbeTPh0bO66tvr7bdEbxaQWngUuH7Iu9ELkUAf8a','pippette@chupanhcuoidep.vn','Yora','Long','84336385948',NULL,1,NULL,'2024-11-13 09:42:51',NULL,0),(99,2,'sviryi','$2a$10$ryb.Txk14EZjhZjI3ModJeTy9ZV27Awl2dtNX3I6iG8KAxu..XINq','sviryi@gmailbrt.com','sviryi1','gmailbrt','84336385949',NULL,1,1,'2024-11-22 04:39:43','2024-11-22 07:04:24',1);
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
  `OAuthProviderAccountId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
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
  UNIQUE KEY `UQ_UC_Provider` (`UserId`,`AuthMethod`,`OAuthProvider`,`OAuthProviderAccountId`),
  CONSTRAINT `FK_UserAccount_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=75 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserAccount`
--

LOCK TABLES `UserAccount` WRITE;
/*!40000 ALTER TABLE `UserAccount` DISABLE KEYS */;
INSERT INTO `UserAccount` VALUES (1,1,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-07-08 10:56:06',NULL,0),(25,48,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 04:04:56',NULL,0),(26,49,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 09:01:47',NULL,0),(27,50,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 09:25:38',NULL,0),(28,51,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 13:16:16',NULL,0),(29,52,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-24 13:27:37',NULL,0),(30,53,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:10:12',NULL,0),(31,54,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:12:53',NULL,0),(32,55,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 08:47:59',NULL,0),(33,56,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-26 10:50:59',NULL,0),(34,57,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-27 02:26:06',NULL,0),(35,58,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:08:17',NULL,0),(36,59,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:11:19',NULL,0),(37,60,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:13:38',NULL,0),(38,61,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:29:20',NULL,0),(39,62,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 06:58:55',NULL,0),(40,63,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 07:19:59',NULL,0),(41,64,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 08:02:31',NULL,0),(42,65,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 08:03:39',NULL,0),(43,66,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-09-30 08:12:05',NULL,0),(44,67,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-01 08:54:00',NULL,0),(45,68,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-04 08:53:00',NULL,0),(48,71,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-22 09:28:50',NULL,0),(49,72,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,48,NULL,'2024-10-25 03:16:26',NULL,0),(50,3,'OAuth','github','163101468',NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-30 08:03:49',NULL,0),(51,73,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 04:46:22',NULL,0),(52,74,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:05:04',NULL,0),(53,75,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:06:45',NULL,0),(54,76,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:08:36',NULL,0),(55,77,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:13:54',NULL,0),(56,78,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:18:15',NULL,0),(57,79,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:21:51',NULL,0),(58,80,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:29:15',NULL,0),(59,81,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:31:20',NULL,0),(60,82,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:31:53',NULL,0),(61,83,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:35:35',NULL,0),(62,84,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:40:23',NULL,0),(63,85,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:45:43',NULL,0),(64,86,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 07:47:35',NULL,0),(65,90,'OAuth','github','1631014788',NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 08:10:53',NULL,0),(66,91,'OAuth','github','1631014789',NULL,NULL,0,NULL,NULL,1,NULL,'2024-10-31 08:13:22',NULL,0),(67,92,'OAuth','github','1631014791',NULL,NULL,0,NULL,NULL,1,1,'2024-11-01 09:13:49','2024-11-13 10:24:17',0),(68,93,'OAuth','github','1631014792',NULL,NULL,0,NULL,NULL,1,1,'2024-11-06 08:31:45','2024-11-06 08:34:54',0),(69,94,'OAuth','github','86759071',NULL,NULL,0,NULL,NULL,1,1,'2024-11-11 11:02:15','2024-11-12 05:19:33',0),(70,95,'OAuth',NULL,NULL,NULL,NULL,0,NULL,NULL,1,1,'2024-11-12 05:08:08','2024-11-19 04:01:26',0),(71,96,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-11-12 08:17:32',NULL,0),(72,97,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-11-12 10:52:07',NULL,0),(73,98,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-11-13 09:42:52',NULL,0),(74,99,'UsernamePassword',NULL,NULL,NULL,NULL,0,NULL,NULL,1,NULL,'2024-11-22 04:39:44',NULL,0);
/*!40000 ALTER TABLE `UserAccount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserSetting`
--

DROP TABLE IF EXISTS `UserSetting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `UserSetting` (
  `SettingId` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `IsNotificationEnable` tinyint unsigned NOT NULL DEFAULT '1',
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`SettingId`),
  KEY `FK_UserSetting_UserId` (`UserId`),
  CONSTRAINT `FK_UserSetting_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserSetting`
--

LOCK TABLES `UserSetting` WRITE;
/*!40000 ALTER TABLE `UserSetting` DISABLE KEYS */;
/*!40000 ALTER TABLE `UserSetting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'SkeletonDB'
--

--
-- Dumping routines for database 'SkeletonDB'
--
/*!50003 DROP PROCEDURE IF EXISTS `GetAllFeaturesWithPermissions` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`magictreedev`@`%` PROCEDURE `GetAllFeaturesWithPermissions`(
    IN RoleArg VARCHAR(100), 
    IN SortArg VARCHAR(100)
)
BEGIN

    SELECT RoleArg REGEXP ';' INTO @RoleArgFlag;
    IF @RoleArgFlag = 1 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in RoleArg';
    END IF;

    SELECT SortArg REGEXP ';' INTO @SortArgFlag;
    IF @SortArgFlag = 1 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in SortArg';
    END IF;

    # Prepare variables for execution
    SET @RoleVar := RoleArg;
    SET @SortVar := IF(SortArg IS NOT NULL AND SortArg <> '', CONCAT('ORDER BY ', SortArg, ' '), 'ORDER BY f.Name ');

    # Prepare query for execution
    SET @GetAllFeaturesWithPermissionsQuery := CONCAT(
        'WITH FeaturePermission_CTE AS (
            SELECT 
                f.FeatureId,
                f.Name AS FeatureName,
                f.Description AS FeatureDescription,
                p.PermissionId,
                p.Name AS PermissionName,
                CASE 
                    WHEN rp.PermissionId IS NOT NULL THEN TRUE 
                    ELSE FALSE 
                END AS Allow
            FROM SkeletonDB.Feature f
            JOIN SkeletonDB.Permission p ON f.FeatureId = p.FeatureId
            LEFT JOIN SkeletonDB.RolePermission rp ON p.PermissionId = rp.PermissionId AND rp.IsDeleted = 0 ', @RoleVar, '
        )
        SELECT 
            fp.FeatureId,
            fp.FeatureName,
            fp.FeatureDescription,
            fp.PermissionId,
            fp.PermissionName,
            fp.Allow
        FROM FeaturePermission_CTE AS fp
        ', @SortVar, ';' 
    );

    PREPARE GetAllFeaturesWithPermissionsQuery FROM @GetAllFeaturesWithPermissionsQuery;
    EXECUTE GetAllFeaturesWithPermissionsQuery;
    DEALLOCATE PREPARE GetAllFeaturesWithPermissionsQuery;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllFeaturesWithPermissions_test` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`magictreedev`@`%` PROCEDURE `GetAllFeaturesWithPermissions_test`(
    IN OffsetArg INT UNSIGNED,
    IN LimitArg INT UNSIGNED,
    IN FilterArg TEXT, 
    IN SortArg VARCHAR(100))
BEGIN

    SELECT FilterArg REGEXP ';' INTO @FilterArgFlag;
    IF @FilterArgFlag = 1 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in FilterArg';
    END IF;

    SELECT SortArg REGEXP ';' INTO @SortArgFlag;
    IF @SortArgFlag = 1 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in SortArg';
    END IF;

    # Prepare variables for excution
    SET @FilterVar := FilterArg;
    SET @SortVar := IF(SortArg IS NOT NULL, CONCAT('ORDER BY ', SortArg, ' '), ' ORDER BY f.Name ');
    SET @OffsetVar := OffsetArg;
    SET @LimitVar := LimitArg;

    # Prepare query for excution
    SET @GetAllFeaturesWithPermissionsQuery := CONCAT(
        'WITH FeaturePermission_CTE AS (
            SELECT 
                f.FeatureId,
                f.Name AS FeatureName,
                f.Description AS FeatureDescription,
                p.PermissionId,
                p.Name AS PermissionName,
                CASE 
                    WHEN rp.PermissionId IS NOT NULL THEN TRUE 
                    ELSE FALSE 
                END AS Allow,
                ROW_NUMBER() OVER (', @SortVar, ') AS row_num
            FROM SkeletonDB.Feature f
            JOIN SkeletonDB.Permission p ON f.FeatureId = p.FeatureId
            LEFT JOIN SkeletonDB.RolePermission rp ON p.PermissionId = rp.PermissionId AND rp.IsDeleted = 0 ', @FilterVar, '
        )
        SELECT 
            fp.FeatureId,
            fp.FeatureName,
            fp.FeatureDescription,
            fp.PermissionId,
            fp.PermissionName,
            fp.Allow
        FROM FeaturePermission_CTE AS fp
        WHERE fp.row_num BETWEEN ', @OffsetVar, ' AND ', @LimitVar, ';'
    );

    PREPARE GetAllFeaturesWithPermissionsQuery FROM @GetAllFeaturesWithPermissionsQuery;
    EXECUTE GetAllFeaturesWithPermissionsQuery;
    DEALLOCATE PREPARE GetAllFeaturesWithPermissionsQuery;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllRolesWithPagination` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`admin`@`%` PROCEDURE `GetAllRolesWithPagination`(
IN OffsetArg int unsigned,
IN LimitArg int unsigned,
IN SearchTextArg text, 
IN SortArg varchar(100))
BEGIN
	SELECT SearchTextArg REGEXP ';' INTO @SearchTextArgFlag;
	IF @SearchTextArgFlag = 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in SearchTextArg';
	END IF;

	SELECT SortArg REGEXP ';' INTO @SortArgFlag;
	IF @SortArgFlag = 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in SortArg';
	END IF;

	# Prepare variables for excution
	SET @SearchTextVar := SearchTextArg;
	SET @SortVar := IF(SortArg IS NOT NULL, CONCAT('ORDER BY ',SortArg, ' '), ' ORDER BY Name ');
	SET @OffsetVar := OffsetArg;
	SET @LimitVar := LimitArg;

	# Prepare query for excution
	SET @GetAllRolesStatement := CONCAT(
	'WITH Role_CTE AS (
		 SELECT 
			RoleId, 
			Name AS Role, 
			Description, 
			IsDeleted,
			ROW_NUMBER() OVER (',@SortVar,') AS row_num
		FROM Role
		',@SearchTextVar,'
	), 
	Count_CTE AS (
		SELECT COUNT(RoleId) AS TotalRecords FROM Role_CTE
	)
	SELECT 
		r.RoleId,
		r.Role,
        r.Description,
		r.IsDeleted,
		c.TotalRecords
	FROM Role_CTE AS r
	CROSS JOIN Count_CTE AS c
	WHERE row_num BETWEEN ',@OffsetVar,' AND ',@LimitVar,';'
	);

	PREPARE GetAllRolesStatement FROM @GetAllRolesStatement;
	EXECUTE GetAllRolesStatement;
	DEALLOCATE PREPARE GetAllRolesStatement;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllUsersWithPagination` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`admin`@`%` PROCEDURE `GetAllUsersWithPagination`(
IN OffsetArg int unsigned,
IN LimitArg int unsigned,
IN FilterArg text, 
IN SortArg varchar(100))
BEGIN
	SELECT FilterArg REGEXP ';' INTO @FilterArgFlag;
	IF @FilterArgFlag = 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in FilterArg';
	END IF;

	SELECT SortArg REGEXP ';' INTO @SortArgFlag;
	IF @SortArgFlag = 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bad SQL in SortArg';
	END IF;

	# Prepare variables for excution
	SET @FilterVar := FilterArg;
	SET @SortVar := IF(SortArg IS NOT NULL, CONCAT('ORDER BY ',SortArg, ' '), ' ORDER BY u.FirstName ');
	SET @OffsetVar := OffsetArg;
	SET @LimitVar := LimitArg;

	# Prepare query for excution
	SET @GetAllUsersStatements := CONCAT(
	'WITH User_CTE AS (
		 SELECT 
			u.UserId,
			u.FirstName,
			u.LastName,
			u.Email,
			u.MobilePhone,
            u.ProfilePictureUrl,
			r.RoleId,
			r.Name AS Role,
			u.IsDeleted,
			ROW_NUMBER() OVER (',@SortVar,') AS row_num
		FROM User AS u
		JOIN Role AS r ON u.RoleId = r.RoleId
		WHERE r.Name != ''System''
		',@FilterVar,'
	), 
	Count_CTE AS (
		SELECT COUNT(UserId) AS TotalRecords FROM User_CTE
	)
	SELECT 
		u.UserId,
		u.FirstName,
		u.LastName,
		u.Email,
		u.MobilePhone,
        u.ProfilePictureUrl,
		u.RoleId,
		u.Role,
		u.IsDeleted,
		c.TotalRecords
	FROM User_CTE AS u
	CROSS JOIN Count_CTE AS c
	WHERE row_num BETWEEN ',@OffsetVar,' AND ',@LimitVar,';'
	);

	PREPARE GetAllUsersStatements FROM @GetAllUsersStatements;
	EXECUTE GetAllUsersStatements;
	DEALLOCATE PREPARE GetAllUsersStatements;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RecordNotification` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`magictreedev`@`%` PROCEDURE `RecordNotification`(
	IN TriggeredUserId	    INT,
    IN NotificationTypeId	BIGINT,
    IN Title				VARCHAR(500),
    IN Content				VARCHAR(500),
    IN SenderInfo			VARCHAR(255),
    IN CreatedBy            INT,
    IN FilterArg			TEXT)
BEGIN
	-- Error handling: Rollback on failure
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL; 
    END;

	START TRANSACTION;

	-- Set variables
	SET @TriggeredUserId := TriggeredUserId;
	SET @NotificationTypeId := NotificationTypeId;
	SET @Title := Title;
	SET @Content := Content;
	SET @SenderInfo := SenderInfo;
	SET @CreatedBy := CreatedBy;
	SET @CurrentDateTime := CURRENT_TIMESTAMP();
    SET @FilterArg := IF(FilterArg IS NOT NULL, FilterArg, ' r.Name IN (''Admin'') ');

	-- Step 1: Insert into Notification table
	SET @InsertNotification := 'INSERT INTO Notification (NotificationTypeId, TriggeredUserId, Title, Content, SenderInfo, CreatedBy, UpdatedBy, CreatedDate, UpdatedDate) VALUES (?,?,?,?,?,?,?,?,?)';
	PREPARE InsertNotificationStatement FROM @InsertNotification;
	EXECUTE InsertNotificationStatement USING @NotificationTypeId, @TriggeredUserId, @Title, @Content, @SenderInfo, @CreatedBy, @CreatedBy, @CurrentDateTime, @CurrentDateTime;
	SET @NotificationId := LAST_INSERT_ID();

	-- Step 2: Insert into NotificationRecipient table
	SET @NotificationRecipient :=   
    CONCAT('INSERT INTO NotificationRecipient (RecipientId, NotificationId, IsRead, CreatedBy, UpdatedBy, CreatedDate, UpdatedDate) 
	SELECT u.UserId, ?, 0, ?, ?, ?, ?
	FROM SkeletonDB.User u
	JOIN SkeletonDB.Role r ON u.RoleId = r.RoleId
	WHERE ', @FilterArg);

	PREPARE NotificationRecipient FROM @NotificationRecipient;
    EXECUTE NotificationRecipient USING @NotificationId, @CreatedBy, @CreatedBy, @CurrentDateTime, @CurrentDateTime;

	COMMIT;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-25 11:00:51
