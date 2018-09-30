USE [master]
GO
/****** Object:  Database [SimpleCourseManagementDb]    Script Date: 30-Sep-18 10:46:59 PM ******/
CREATE DATABASE [SimpleCourseManagementDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
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
ALTER DATABASE [SimpleCourseManagementDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleCourseManagementDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleCourseManagementDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SimpleCourseManagementDb]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 30-Sep-18 10:47:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Batch](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[BatchCode] [varchar](50) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Instructor] [varchar](150) NOT NULL,
	[CourseDetails] [varchar](max) NULL,
	[Duration] [int] NOT NULL,
	[CourseFee] [float] NOT NULL,
	[TotalSeat] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[UserDetailsId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 30-Sep-18 10:47:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseCode] [varchar](50) NOT NULL,
	[CourseName] [varchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserDetailsId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trainee]    Script Date: 30-Sep-18 10:47:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trainee](
	[TraineeId] [int] IDENTITY(1,1) NOT NULL,
	[TraineeCode] [varchar](50) NOT NULL,
	[BatchId] [int] NOT NULL,
	[TraineeName] [varchar](150) NOT NULL,
	[TraineeImage] [varchar](max) NULL,
	[FatherName] [varchar](150) NOT NULL,
	[MotherName] [varchar](150) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[Age] [float] NOT NULL,
	[Address] [varchar](500) NOT NULL,
	[ContactNumber] [varchar](20) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[NationalIdCard] [varchar](50) NOT NULL,
	[Result] [varchar](150) NULL,
	[UserDetailsId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Trainee] PRIMARY KEY CLUSTERED 
(
	[TraineeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 30-Sep-18 10:47:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](150) NOT NULL,
	[LoginName] [varchar](50) NOT NULL,
	[LoginPassword] [varchar](max) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 30-Sep-18 10:47:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[UserDetails] ON 

INSERT [dbo].[UserDetails] ([UserDetailsId], [UserName], [LoginName], [LoginPassword], [UserRoleId], [IsActive]) VALUES (1, N'Ashiqur Rahman', N'ashiq', N'1', 1, 1)
INSERT [dbo].[UserDetails] ([UserDetailsId], [UserName], [LoginName], [LoginPassword], [UserRoleId], [IsActive]) VALUES (2, N'Tasfiqur Rahman', N'tasfiq', N'2', 2, 1)
SET IDENTITY_INSERT [dbo].[UserDetails] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[UserRole] ([UserRoleId], [Role]) VALUES (2, N'Executive')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Batch_BatchCode]    Script Date: 30-Sep-18 10:47:01 PM ******/
ALTER TABLE [dbo].[Batch] ADD  CONSTRAINT [UK_Batch_BatchCode] UNIQUE NONCLUSTERED 
(
	[BatchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Course_CourseCode]    Script Date: 30-Sep-18 10:47:01 PM ******/
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [UK_Course_CourseCode] UNIQUE NONCLUSTERED 
(
	[CourseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Trainee_Email]    Script Date: 30-Sep-18 10:47:01 PM ******/
ALTER TABLE [dbo].[Trainee] ADD  CONSTRAINT [UK_Trainee_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Trainee_NationalIdCard]    Script Date: 30-Sep-18 10:47:01 PM ******/
ALTER TABLE [dbo].[Trainee] ADD  CONSTRAINT [UK_Trainee_NationalIdCard] UNIQUE NONCLUSTERED 
(
	[NationalIdCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Trainee_TraineeCode]    Script Date: 30-Sep-18 10:47:01 PM ******/
ALTER TABLE [dbo].[Trainee] ADD  CONSTRAINT [UK_Trainee_TraineeCode] UNIQUE NONCLUSTERED 
(
	[TraineeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Course]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_UserDetails] FOREIGN KEY([UserDetailsId])
REFERENCES [dbo].[UserDetails] ([UserDetailsId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_UserDetails]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_UserDetails] FOREIGN KEY([UserDetailsId])
REFERENCES [dbo].[UserDetails] ([UserDetailsId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_UserDetails]
GO
ALTER TABLE [dbo].[Trainee]  WITH CHECK ADD  CONSTRAINT [FK_Trainee_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[Trainee] CHECK CONSTRAINT [FK_Trainee_Batch]
GO
ALTER TABLE [dbo].[Trainee]  WITH CHECK ADD  CONSTRAINT [FK_Trainee_UserDetails] FOREIGN KEY([UserDetailsId])
REFERENCES [dbo].[UserDetails] ([UserDetailsId])
GO
ALTER TABLE [dbo].[Trainee] CHECK CONSTRAINT [FK_Trainee_UserDetails]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([UserRoleId])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_UserRole]
GO
USE [master]
GO
ALTER DATABASE [SimpleCourseManagementDb] SET  READ_WRITE 
GO
