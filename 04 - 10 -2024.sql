CREATE DATABASE PRN231_DB_Job_Recruitment;
USE PRN231_DB_Job_Recruitment;

-- Table creation for Roles
CREATE TABLE Roles (
    RoleID UNIQUEIDENTIFIER PRIMARY KEY,
    RoleName VARCHAR(255)
);

-- Table creation for Accounts
CREATE TABLE Accounts (
    AccountID UNIQUEIDENTIFIER PRIMARY KEY,
    RoleID UNIQUEIDENTIFIER,
    Username VARCHAR(255),
    PasswordHash NVARCHAR(255),
    FullName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Address VARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(50),
    ProfilePicture NVARCHAR(255)
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Table creation for Resumes
CREATE TABLE Resumes (
    ResumeID UNIQUEIDENTIFIER PRIMARY KEY,
    AccountID UNIQUEIDENTIFIER,
    ResumeURL NVARCHAR(255),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

-- Table creation for JobTypes
CREATE TABLE JobTypes (
    JobTypeID UNIQUEIDENTIFIER PRIMARY KEY,
    JobTypeName VARCHAR(255)
);

-- Table creation for JobCategories
CREATE TABLE JobCategories (
    JobCategoryID UNIQUEIDENTIFIER PRIMARY KEY,
    JobCategoryName VARCHAR(255)
);

-- Table creation for JobLevels
CREATE TABLE JobLevels (
    JobLevelID UNIQUEIDENTIFIER PRIMARY KEY,
    JobLevelName VARCHAR(255)
);

-- Table creation for JobSkills
CREATE TABLE JobSkills (
    JobSkillID UNIQUEIDENTIFIER PRIMARY KEY,
    SkillName VARCHAR(255)
);
-- Table creation for Jobs
CREATE TABLE Jobs (
    JobID UNIQUEIDENTIFIER PRIMARY KEY,
    JobTitle VARCHAR(255),
    JobDescription TEXT,
    JobRequirements TEXT,
    JobBenefits TEXT,
    JobTime VARCHAR(255),
    SalaryRange VARCHAR(255),
    ExperiencesRequired VARCHAR(255),
    Deadline DATE,
    Status VARCHAR(50),
    DateCreated DATE,
    RecruiterID UNIQUEIDENTIFIER,
    JobTypeID UNIQUEIDENTIFIER,
    JobCategoryID UNIQUEIDENTIFIER,
    JobLevelID UNIQUEIDENTIFIER,
    FOREIGN KEY (RecruiterID) REFERENCES Accounts(AccountID),
    FOREIGN KEY (JobTypeID) REFERENCES JobTypes(JobTypeID),
    FOREIGN KEY (JobCategoryID) REFERENCES JobCategories(JobCategoryID),
    FOREIGN KEY (JobLevelID) REFERENCES JobLevels(JobLevelID)
);

-- Composite table for Jobs and JobSkills
CREATE TABLE JobSkillsMapping (
    JobID UNIQUEIDENTIFIER,
    JobSkillID UNIQUEIDENTIFIER,
    ExperienceRequired VARCHAR(255),
    PRIMARY KEY (JobID, JobSkillID),
    FOREIGN KEY (JobID) REFERENCES Jobs(JobID),
    FOREIGN KEY (JobSkillID) REFERENCES JobSkills(JobSkillID)
);

-- Composite table for Accounts and JobSkills
CREATE TABLE AccountSkillsMapping (
    AccountID UNIQUEIDENTIFIER,
    JobSkillID UNIQUEIDENTIFIER,
    Experienced VARCHAR(255),
	PRIMARY KEY (AccountID, JobSkillID),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID),
    FOREIGN KEY (JobSkillID) REFERENCES JobSkills(JobSkillID)
);

-- Table creation for Applications
CREATE TABLE Applications (
    ApplicationID UNIQUEIDENTIFIER PRIMARY KEY,
    JobID UNIQUEIDENTIFIER,
    AccountID UNIQUEIDENTIFIER,
    ApplicationDate DATE,
    Status VARCHAR(50),
    FOREIGN KEY (JobID) REFERENCES Jobs(JobID),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

-- Table creation for InterviewProcess
CREATE TABLE InterviewProcess (
    InterviewID UNIQUEIDENTIFIER PRIMARY KEY,
    ApplicationID UNIQUEIDENTIFIER,
    InterviewDate DATE,
    Interviewer VARCHAR(255),
    InterviewResult VARCHAR(255),
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID)
);
