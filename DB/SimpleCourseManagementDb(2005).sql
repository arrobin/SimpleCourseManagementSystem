USE [master]
GO
/****** Object:  Database [SimpleCourseManagementDb]    Script Date: 05-Oct-18 11:32:45 AM ******/
CREATE DATABASE [SimpleCourseManagementDb] ON  PRIMARY 
( NAME = N'SimpleCourseManagementDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SimpleCourseManagementDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SimpleCourseManagementDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SimpleCourseManagementDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleCourseManagementDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleCourseManagementDb] SET DB_CHAINING OFF 
GO
