USE [master]
GO
/****** Object:  Database [PRN231_DB_Job_Recruitment]    Script Date: 9/21/2024 2:06:42 PM ******/
CREATE DATABASE [PRN231_DB_Job_Recruitment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN231_DB_Job_Recruitment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PRN231_DB_Job_Recruitment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN231_DB_Job_Recruitment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PRN231_DB_Job_Recruitment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN231_DB_Job_Recruitment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET RECOVERY FULL 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET  MULTI_USER 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET QUERY_STORE = OFF
GO
USE [PRN231_DB_Job_Recruitment]
GO
/****** Object:  User [saa]    Script Date: 9/21/2024 2:06:42 PM ******/
CREATE USER [saa] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sa1]    Script Date: 9/21/2024 2:06:42 PM ******/
CREATE USER [sa1] FOR LOGIN [sa1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sa]    Script Date: 9/21/2024 2:06:42 PM ******/
CREATE USER [sa] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 9/21/2024 2:06:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[accountId] [int] IDENTITY(1,1) NOT NULL,
	[passwordHash] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[dateCreate] [datetime] NULL,
	[accessToken] [nvarchar](255) NULL,
	[roleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountJobkill]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountJobkill](
	[accountJobkillId] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[JobkillId] [int] NULL,
	[experience] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[accountJobkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[applicationId] [int] IDENTITY(1,1) NOT NULL,
	[applicationDate] [datetime] NULL,
	[status] [nvarchar](50) NULL,
	[cvId] [int] NULL,
	[jobId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[applicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certificate](
	[certificateId] [int] IDENTITY(1,1) NOT NULL,
	[certificateUrl] [nvarchar](255) NULL,
	[accountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[certificateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CV]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CV](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [nvarchar](500) NULL,
	[urlCV] [nvarchar](255) NULL,
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HR]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HR](
	[hrId] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [nvarchar](255) NULL,
	[phone] [nvarchar](20) NULL,
	[address] [nvarchar](255) NULL,
	[position] [nvarchar](255) NULL,
	[status] [nvarchar](50) NULL,
	[accountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[hrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewProcess]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewProcess](
	[processId] [int] IDENTITY(1,1) NOT NULL,
	[roundNumber] [int] NULL,
	[dateCreated] [datetime] NULL,
	[details] [nvarchar](500) NULL,
	[status] [nvarchar](50) NULL,
	[recruiterId] [int] NULL,
	[applicantId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[processId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[jobId] [int] IDENTITY(1,1) NOT NULL,
	[jobTitle] [nvarchar](255) NULL,
	[description] [nvarchar](500) NULL,
	[requirement] [nvarchar](500) NULL,
	[benefit] [nvarchar](500) NULL,
	[jobTime] [nvarchar](100) NULL,
	[salaryRange] [nvarchar](100) NULL,
	[experience] [nvarchar](100) NULL,
	[deadline] [datetime] NULL,
	[status] [nvarchar](50) NULL,
	[dateCreated] [datetime] NULL,
	[accountId] [int] NULL,
	[JobTypeId] [int] NULL,
	[JobLevelId] [int] NULL,
	[JobCategoryId] [int] NULL,
	[JobkillId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[jobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCategory]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobkill]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobkill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobLevel]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobType]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/21/2024 2:06:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NULL,
	[gender] [nvarchar](10) NULL,
	[fullname] [nvarchar](255) NULL,
	[phone] [nvarchar](20) NULL,
	[location] [nvarchar](255) NULL,
	[dateCreate] [datetime] NULL,
	[accountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([accountId], [passwordHash], [email], [dateCreate], [accessToken], [roleId]) VALUES (1, N'232002', N'hieuhtse162342093@fpt.edu.vn', NULL, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6IjExZTJjZjIxLWVhODctNDM0MS04NzJiLTU0MTU0MGQzMmExOCIsImV4cCI6MTcyNjg1NDE0MSwiaXNzIjoiSW5kaWVHYW1lSXNzdWVyIiwiYXVkIjoiSW5kaWVHYW1lQXVkaWVuY2UifQ.7YhaD1M_zfMLyXsjtDX354Uf-Hocl6wgMjyUQ0FTA1c', NULL)
INSERT [dbo].[Account] ([accountId], [passwordHash], [email], [dateCreate], [accessToken], [roleId]) VALUES (2, N'232002', N'hieuhtse16234ww32093@fpt.edu.vn', NULL, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6ImQ2Zjk2YjA0LTg2Y2MtNGZhNy1iMjA4LTQyNzQzOTNmMmJlOSIsImV4cCI6MTcyNjg1NDE1NywiaXNzIjoiSW5kaWVHYW1lSXNzdWVyIiwiYXVkIjoiSW5kaWVHYW1lQXVkaWVuY2UifQ.GbCCAp8j_UCHo_eSUCGapYCS-d1SroFulXg-UYRkfIE', NULL)
INSERT [dbo].[Account] ([accountId], [passwordHash], [email], [dateCreate], [accessToken], [roleId]) VALUES (3, N'41223', N'trongtlhse160536@fpt.edu.vn', NULL, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6Ijk0OTg3NjViLTk0M2QtNGZjMy04MmJiLTlmMWQ0YTlhYzhjOSIsImV4cCI6MTcyNjkwMDQ5NSwiaXNzIjoiSW5kaWVHYW1lSXNzdWVyIiwiYXVkIjoiSW5kaWVHYW1lQXVkaWVuY2UifQ.IpgAqPjbGs-X9YWJ9_dvyIv9uaJvvCDTeTtowD-LPS4', NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[AccountJobkill]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[AccountJobkill]  WITH CHECK ADD FOREIGN KEY([JobkillId])
REFERENCES [dbo].[Jobkill] ([Id])
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD FOREIGN KEY([jobId])
REFERENCES [dbo].[Job] ([jobId])
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD FOREIGN KEY([cvId])
REFERENCES [dbo].[CV] ([id])
GO
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[CV]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[HR]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[InterviewProcess]  WITH CHECK ADD FOREIGN KEY([applicantId])
REFERENCES [dbo].[Application] ([applicationId])
GO
ALTER TABLE [dbo].[InterviewProcess]  WITH CHECK ADD FOREIGN KEY([recruiterId])
REFERENCES [dbo].[HR] ([hrId])
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD FOREIGN KEY([JobCategoryId])
REFERENCES [dbo].[JobCategory] ([Id])
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD FOREIGN KEY([JobLevelId])
REFERENCES [dbo].[JobLevel] ([Id])
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD FOREIGN KEY([JobTypeId])
REFERENCES [dbo].[JobType] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
USE [master]
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET  READ_WRITE 
GO
