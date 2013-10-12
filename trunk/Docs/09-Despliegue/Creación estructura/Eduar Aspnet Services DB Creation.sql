SET NOCOUNT ON
GO

USE [master]
GO

if exists (select * from sysdatabases where name=N'EDUAR_aspnet_services')
        drop database EDUAR
GO
DECLARE @device_directory NVARCHAR(520)

SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

/****** Object:  Database [EDUAREDUAR_aspnet_services    Script Date: 09/10/2013 19:51:43 ******/
EXECUTE (N'CREATE DATABASE [EDUAR_aspnet_services]
  ON PRIMARY (NAME = N''EDUAR_aspnet_services'', FILENAME = N''' + @device_directory + 'EDUAR_aspnet_services.mdf'', SIZE = 11392KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB)
  LOG ON (NAME = N''EDUAR_aspnet_services_log'',  FILENAME = N''' + @device_directory + 'EDUAR_aspnet_services_log.ldf'' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)')

GO

ALTER DATABASE [EDUAR_aspnet_services] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EDUAR_aspnet_services].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ARITHABORT OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET  DISABLE_BROKER 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET  MULTI_USER 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [EDUAR_aspnet_services] SET DB_CHAINING OFF 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [EDUAR_aspnet_services] SET  READ_WRITE 
GO