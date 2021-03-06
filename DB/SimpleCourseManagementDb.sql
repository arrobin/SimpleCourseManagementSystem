USE [master]
GO
/****** Object:  Database [SimpleCourseManagementDb]    Script Date: 26-Oct-18 6:06:56 AM ******/
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
/****** Object:  Table [dbo].[Batch]    Script Date: 26-Oct-18 6:06:56 AM ******/
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
	[Time] [varchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[Course]    Script Date: 26-Oct-18 6:06:57 AM ******/
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
/****** Object:  Table [dbo].[Trainee]    Script Date: 26-Oct-18 6:06:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trainee](
	[TraineeId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[TraineeCourse]    Script Date: 26-Oct-18 6:06:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TraineeCourse](
	[TraineeCourseId] [int] IDENTITY(1,1) NOT NULL,
	[TraineeCode] [varchar](50) NOT NULL,
	[TraineeId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[BatchId] [int] NOT NULL,
	[Result] [varchar](50) NULL,
	[UserDetailsId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TraineeCourse] PRIMARY KEY CLUSTERED 
(
	[TraineeCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 26-Oct-18 6:06:57 AM ******/
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
/****** Object:  Table [dbo].[UserRole]    Script Date: 26-Oct-18 6:06:57 AM ******/
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
SET IDENTITY_INSERT [dbo].[Batch] ON 

INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (1, N'001', 3, CAST(N'2001-01-01' AS Date), CAST(N'2001-01-01' AS Date), N'3:00PM-6:00PM', N'Tasfiqur Rahman', N'1. Course Details-1
2. Course Details-2
3. Course Details-3
4. Course Details-4
5. Course Details-5', 25, 20000, 30, N'', 1, CAST(N'2018-10-16 01:10:59.060' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (2, N'002', 2, CAST(N'2018-10-30' AS Date), CAST(N'2018-12-30' AS Date), N'1:00PM-4:00PM', N'MD. Salauddin', N'Test Data', 25, 25000, 35, N'', 1, CAST(N'2018-10-25 22:23:51.833' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (3, N'003', 6, CAST(N'2018-11-15' AS Date), CAST(N'2018-12-31' AS Date), N'10:00AM-12:00PM', N'Rashedul Islam', N'Test Data', 100, 15000, 40, N'', 1, CAST(N'2018-10-11 19:51:06.970' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (1002, N'April-Android', 9, CAST(N'2018-10-15' AS Date), CAST(N'2018-11-30' AS Date), N'03pm-05pm', N'Mashrafee-Bin-Mortuza', N'Game development
Mobile App development', 120, 15000, 25, N'', 1, CAST(N'2018-10-12 10:55:18.253' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (1003, N'PHP-October', 10, CAST(N'2018-10-30' AS Date), CAST(N'2019-01-01' AS Date), N'03:00pm-5:00pm', N'Shifo', N'Oop', 300, 15000, 30, N'', 1, CAST(N'2018-10-12 11:18:25.500' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (1004, N'JAVA-October', 11, CAST(N'2018-10-30' AS Date), CAST(N'2019-01-01' AS Date), N'10:00am-2:00pm', N'Mashrafee', N'OOP', 300, 15000, 30, N'', 1, CAST(N'2018-10-12 11:45:20.027' AS DateTime))
INSERT [dbo].[Batch] ([BatchId], [BatchCode], [CourseId], [StartDate], [EndDate], [Time], [Instructor], [CourseDetails], [Duration], [CourseFee], [TotalSeat], [Status], [UserDetailsId], [CreatedDateTime]) VALUES (1005, N'DotNet-October', 1, CAST(N'2018-10-01' AS Date), CAST(N'2018-11-30' AS Date), N'03:00pm-5:00pm', N'Rahim', N'1.aaaaa,
2. bbbbbbbb,
3. ccccccccc,
4. rrrrrrrrr', 60, 15000, 20, N'', 1, CAST(N'2018-10-26 05:51:47.450' AS DateTime))
SET IDENTITY_INSERT [dbo].[Batch] OFF
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (1, N'DotNet001', N'Dot Net Web Application MVC', 1, 1, CAST(N'2018-10-05 16:05:07.577' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (2, N'PHPLaravel001', N'Web Application MVC Laravel', 1, 1, CAST(N'2018-10-03 00:23:00.843' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (3, N'DotNet011', N'Dot Net Web Application MVC', 1, 1, CAST(N'2018-10-08 22:19:50.580' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (5, N'PHPLaravel002', N'Laravel Web Application MVC', 1, 1, CAST(N'2018-10-03 23:05:03.947' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (6, N'001', N'Windows Form Application', 1, 1, CAST(N'2018-10-12 11:42:04.537' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (8, N'002', N'Python', 0, 1, CAST(N'2018-10-11 12:01:08.263' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (9, N'Android001', N'Andriod', 1, 1, CAST(N'2018-10-12 10:50:05.857' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (10, N'PHP-001', N'PHP', 1, 1, CAST(N'2018-10-12 11:41:52.707' AS DateTime))
INSERT [dbo].[Course] ([CourseId], [CourseCode], [CourseName], [IsActive], [UserDetailsId], [CreatedDateTime]) VALUES (11, N'JAVA-005', N'JAVA', 1, 1, CAST(N'2018-10-12 11:40:56.743' AS DateTime))
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Trainee] ON 

INSERT [dbo].[Trainee] ([TraineeId], [TraineeName], [TraineeImage], [FatherName], [MotherName], [Gender], [Age], [Address], [ContactNumber], [Email], [NationalIdCard], [UserDetailsId], [CreatedDateTime]) VALUES (9, N'Md.Kyum Ahmed', N'~/Content/TraineeImages/263584180628697.jpg', N'Shahana Islam', N'Md. Farhad Ahmed', N'Male', 26, N'Jatrabari', N'01677885544', N'kyum@gmail.com', N'123456789', 2, CAST(N'2018-10-25 23:06:54.050' AS DateTime))
INSERT [dbo].[Trainee] ([TraineeId], [TraineeName], [TraineeImage], [FatherName], [MotherName], [Gender], [Age], [Address], [ContactNumber], [Email], [NationalIdCard], [UserDetailsId], [CreatedDateTime]) VALUES (11, N'Masrafee Bin Mortaza', N'~/Content/TraineeImages/260px-Mashrafe_Bin_Mortaza_(cropped)181257315.jpg', N'Md. Obaidur Rahman', N'Nargis Akter', N'Male', 33, N'Uttara', N'01911111111', N'masrafee@gmail.com', N'95666765676', 2, CAST(N'2018-10-26 03:12:57.353' AS DateTime))
INSERT [dbo].[Trainee] ([TraineeId], [TraineeName], [TraineeImage], [FatherName], [MotherName], [Gender], [Age], [Address], [ContactNumber], [Email], [NationalIdCard], [UserDetailsId], [CreatedDateTime]) VALUES (13, N'Mahfuza Akter', N'~/Content/TraineeImages/Capture182555377.PNG', N'Md. Sharif Hossain', N'Kulsum Khatun', N'Female', 25, N'Jatrabari', N'01933333333', N'ttt@gmail.com', N'1122334455', 2, CAST(N'2018-10-26 03:25:55.403' AS DateTime))
SET IDENTITY_INSERT [dbo].[Trainee] OFF
SET IDENTITY_INSERT [dbo].[TraineeCourse] ON 

INSERT [dbo].[TraineeCourse] ([TraineeCourseId], [TraineeCode], [TraineeId], [CourseId], [BatchId], [Result], [UserDetailsId], [CreatedDateTime]) VALUES (1, N'001', 9, 10, 1, N' A', 2, CAST(N'2018-10-25 23:07:00.457' AS DateTime))
INSERT [dbo].[TraineeCourse] ([TraineeCourseId], [TraineeCode], [TraineeId], [CourseId], [BatchId], [Result], [UserDetailsId], [CreatedDateTime]) VALUES (2, N'002', 9, 9, 1002, N'B', 2, CAST(N'2018-10-26 03:00:25.803' AS DateTime))
INSERT [dbo].[TraineeCourse] ([TraineeCourseId], [TraineeCode], [TraineeId], [CourseId], [BatchId], [Result], [UserDetailsId], [CreatedDateTime]) VALUES (3, N'003', 11, 3, 1, N' A', 2, CAST(N'2018-10-26 03:12:57.357' AS DateTime))
INSERT [dbo].[TraineeCourse] ([TraineeCourseId], [TraineeCode], [TraineeId], [CourseId], [BatchId], [Result], [UserDetailsId], [CreatedDateTime]) VALUES (5, N'004', 13, 3, 1, N'C', 2, CAST(N'2018-10-26 03:25:55.407' AS DateTime))
INSERT [dbo].[TraineeCourse] ([TraineeCourseId], [TraineeCode], [TraineeId], [CourseId], [BatchId], [Result], [UserDetailsId], [CreatedDateTime]) VALUES (7, N'006', 9, 2, 2, N'', 2, CAST(N'2018-10-26 05:44:52.613' AS DateTime))
SET IDENTITY_INSERT [dbo].[TraineeCourse] OFF
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
/****** Object:  Index [UK_Batch_BatchCode]    Script Date: 26-Oct-18 6:06:57 AM ******/
ALTER TABLE [dbo].[Batch] ADD  CONSTRAINT [UK_Batch_BatchCode] UNIQUE NONCLUSTERED 
(
	[BatchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Course_CourseCode]    Script Date: 26-Oct-18 6:06:57 AM ******/
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [UK_Course_CourseCode] UNIQUE NONCLUSTERED 
(
	[CourseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Trainee_Email]    Script Date: 26-Oct-18 6:06:57 AM ******/
ALTER TABLE [dbo].[Trainee] ADD  CONSTRAINT [UK_Trainee_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Trainee_NationalIdCard]    Script Date: 26-Oct-18 6:06:57 AM ******/
ALTER TABLE [dbo].[Trainee] ADD  CONSTRAINT [UK_Trainee_NationalIdCard] UNIQUE NONCLUSTERED 
(
	[NationalIdCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_TraineeCode]    Script Date: 26-Oct-18 6:06:57 AM ******/
ALTER TABLE [dbo].[TraineeCourse] ADD  CONSTRAINT [UK_TraineeCode] UNIQUE NONCLUSTERED 
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
ALTER TABLE [dbo].[Trainee]  WITH CHECK ADD  CONSTRAINT [FK_Trainee_UserDetails] FOREIGN KEY([UserDetailsId])
REFERENCES [dbo].[UserDetails] ([UserDetailsId])
GO
ALTER TABLE [dbo].[Trainee] CHECK CONSTRAINT [FK_Trainee_UserDetails]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_Batch]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_Course]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_Trainee] FOREIGN KEY([TraineeId])
REFERENCES [dbo].[Trainee] ([TraineeId])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_Trainee]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_UserDetails] FOREIGN KEY([UserDetailsId])
REFERENCES [dbo].[UserDetails] ([UserDetailsId])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_UserDetails]
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
