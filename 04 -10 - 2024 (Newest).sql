create database [PRN231_DB_Job_Recruitment]

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

/****** Object:  Table [dbo].[Account]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[accountId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](255) NULL,
	[passwordHash] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[fullName] [nvarchar](255) NULL,
	[phoneNumber] [varchar](20) NULL,
	[address] [nvarchar](255) NULL,
	[dateOfBirth] [date] NULL,
	[urlPicture] [nvarchar](max) NULL,
	[gender] [varchar](50) NULL,
	[dateCreate] [datetime] NULL,
	[roleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resume](
	[resumeId] [int] IDENTITY(1,1) NOT NULL,
	[urlResume] [nvarchar](max) NULL,
	[accountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[resumeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountJobSkill]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountJobSkill](
	[AccountJobSkillId] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[jobSkillId] [int] NULL,
	[experience] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountJobSkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[applicationId] [int] IDENTITY(1,1) NOT NULL,
	[applicationDate] [datetime] NULL,
	[status] [nvarchar](50) NULL,
	[accountId] [int] NULL,
	[jobId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[applicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 10/2/2024 3:26:56 PM ******/
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

/****** Object:  Table [dbo].[InterviewProcess]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewProcess](
	[interviewProcessId] [int] IDENTITY(1,1) NOT NULL,
	[roundNumber] [int] NULL,
	[interviewDate] [datetime] NULL,
	[detail] [nvarchar](500) NULL,
	[result] [nvarchar](500) NULL,
	[dateCreated] [datetime] NULL,
	[accountId] [int] NULL,
	[applicationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[interviewProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 10/2/2024 3:26:56 PM ******/
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
	[jobSkillId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[jobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCategory]    Script Date: 10/2/2024 3:26:56 PM ******/
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
/****** Object:  Table [dbo].[JobJobSkill]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobJobSkill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[JobSkillId] [int] NULL,
	[experienceRequired] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobSkill]    Script Date: 10/2/2024 3:26:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobSkill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobLevel]    Script Date: 10/2/2024 3:26:56 PM ******/
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
/****** Object:  Table [dbo].[JobType]    Script Date: 10/2/2024 3:26:56 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 10/2/2024 3:26:56 PM ******/
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

ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[Resume]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[AccountJobSkill]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[AccountJobSkill]  WITH CHECK ADD FOREIGN KEY([jobSkillId])
REFERENCES [dbo].[jobSkill] ([Id])
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD FOREIGN KEY([jobId])
REFERENCES [dbo].[Job] ([jobId])
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
GO

ALTER TABLE [dbo].[InterviewProcess]  WITH CHECK ADD FOREIGN KEY([applicationId])
REFERENCES [dbo].[Application] ([applicationId])
GO

ALTER TABLE [dbo].[InterviewProcess]  WITH CHECK ADD FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([accountId])
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
ALTER TABLE [dbo].[JobJobSkill]  WITH CHECK ADD FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([jobId])
GO
ALTER TABLE [dbo].[JobJobSkill]  WITH CHECK ADD FOREIGN KEY([JobSkillId])
REFERENCES [dbo].[jobSkill] ([Id])
GO

USE [master]
GO
ALTER DATABASE [PRN231_DB_Job_Recruitment] SET  READ_WRITE 
GO
