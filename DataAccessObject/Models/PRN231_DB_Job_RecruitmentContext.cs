﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObject.Models;

public partial class PRN231_DB_Job_RecruitmentContext : DbContext
{
    public PRN231_DB_Job_RecruitmentContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "PRN231_FALL2024_GROUP6_API"))
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }
    public PRN231_DB_Job_RecruitmentContext(DbContextOptions<PRN231_DB_Job_RecruitmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountJobSkill> AccountJobSkills { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Educate> Educates { get; set; }

    public virtual DbSet<InterviewProcess> InterviewProcesses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<JobJobSkill> JobJobSkills { get; set; }

    public virtual DbSet<JobLevel> JobLevels { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__F267251EBD2F3296");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("dateCreate");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Major)
                .HasMaxLength(255)
                .HasColumnName("major");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UrlPicture).HasColumnName("urlPicture");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__roleId__5070F446");
        });

        modelBuilder.Entity<AccountJobSkill>(entity =>
        {
            entity.HasKey(e => e.AccountJobSkillId).HasName("PK__AccountJ__978CAB0832C1ABFC");

            entity.ToTable("AccountJobSkill");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Experience)
                .HasMaxLength(100)
                .HasColumnName("experience");
            entity.Property(e => e.JobSkillId).HasColumnName("jobSkillId");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountJobSkills)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__AccountJo__accou__5165187F");

            entity.HasOne(d => d.JobSkill).WithMany(p => p.AccountJobSkills)
                .HasForeignKey(d => d.JobSkillId)
                .HasConstraintName("FK__AccountJo__jobSk__52593CB8");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__79FDB1CF512F807D");

            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId).HasColumnName("applicationId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime")
                .HasColumnName("applicationDate");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UrlCv).HasColumnName("urlCV");

            entity.HasOne(d => d.Account).WithMany(p => p.Applications)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Applicati__accou__534D60F1");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Applicati__jobId__5441852A");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__A15CBEAE1C01CEAE");

            entity.ToTable("Certificate");

            entity.Property(e => e.CertificateId).HasColumnName("certificateId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CertificateName)
                .HasMaxLength(255)
                .HasColumnName("certificateName");
            entity.Property(e => e.CertificateUrl)
                .HasMaxLength(255)
                .HasColumnName("certificateUrl");

            entity.HasOne(d => d.Account).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Certifica__accou__5535A963");
        });

        modelBuilder.Entity<Educate>(entity =>
        {
            entity.HasKey(e => e.EducateId).HasName("PK__Educate__CBF6E7174086D917");

            entity.ToTable("Educate");

            entity.Property(e => e.EducateId).HasColumnName("educateId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EducateName)
                .HasMaxLength(255)
                .HasColumnName("educateName");

            entity.HasOne(d => d.Account).WithMany(p => p.Educates)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Educate__account__5629CD9C");
        });

        modelBuilder.Entity<InterviewProcess>(entity =>
        {
            entity.HasKey(e => e.InterviewProcessId).HasName("PK__Intervie__6ED07DF7935C58B2");

            entity.ToTable("InterviewProcess");

            entity.Property(e => e.InterviewProcessId).HasColumnName("interviewProcessId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.ApplicationId).HasColumnName("applicationId");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Detail)
                .HasMaxLength(500)
                .HasColumnName("detail");
            entity.Property(e => e.InterviewDate)
                .HasColumnType("datetime")
                .HasColumnName("interviewDate");
            entity.Property(e => e.Result)
                .HasMaxLength(500)
                .HasColumnName("result");
            entity.Property(e => e.RoundNumber).HasColumnName("roundNumber");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.InterviewProcesses)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Interview__accou__571DF1D5");

            entity.HasOne(d => d.Application).WithMany(p => p.InterviewProcesses)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__Interview__appli__5812160E");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__164AA1A876D22B95");

            entity.ToTable("Job");

            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Benefit)
                .HasMaxLength(500)
                .HasColumnName("benefit");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Experience)
                .HasMaxLength(100)
                .HasColumnName("experience");
            entity.Property(e => e.JobSkillId).HasColumnName("jobSkillId");
            entity.Property(e => e.JobTime)
                .HasMaxLength(100)
                .HasColumnName("jobTime");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("jobTitle");
            entity.Property(e => e.Location)
                .HasMaxLength(500)
                .HasColumnName("location");
            entity.Property(e => e.QuantityCurrent).HasColumnName("quantityCurrent");
            entity.Property(e => e.QuantityMax).HasColumnName("quantityMax");
            entity.Property(e => e.Requirement)
                .HasMaxLength(500)
                .HasColumnName("requirement");
            entity.Property(e => e.SalaryRange)
                .HasMaxLength(100)
                .HasColumnName("salaryRange");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UrlPicture).HasColumnName("urlPicture");

            entity.HasOne(d => d.Account).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Job__accountId__59063A47");

            entity.HasOne(d => d.JobCategory).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobCategoryId)
                .HasConstraintName("FK__Job__JobCategory__59FA5E80");

            entity.HasOne(d => d.JobLevel).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobLevelId)
                .HasConstraintName("FK__Job__JobLevelId__5AEE82B9");

            entity.HasOne(d => d.JobType).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobTypeId)
                .HasConstraintName("FK__Job__JobTypeId__5BE2A6F2");
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobCateg__3214EC075F687ADE");

            entity.ToTable("JobCategory");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<JobJobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobJobSk__3214EC07AA45C81C");

            entity.ToTable("JobJobSkill");

            entity.Property(e => e.ExperienceRequired)
                .HasMaxLength(255)
                .HasColumnName("experienceRequired");

            entity.HasOne(d => d.Job).WithMany(p => p.JobJobSkills)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__JobJobSki__JobId__5CD6CB2B");

            entity.HasOne(d => d.JobSkill).WithMany(p => p.JobJobSkills)
                .HasForeignKey(d => d.JobSkillId)
                .HasConstraintName("FK__JobJobSki__JobSk__5DCAEF64");
        });

        modelBuilder.Entity<JobLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobLevel__3214EC07B217F692");

            entity.ToTable("JobLevel");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jobSkill__3214EC07A78D2A93");

            entity.ToTable("jobSkill");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobType__3214EC07A1272C7D");

            entity.ToTable("JobType");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__4BA5CEA9AB17C7F7");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("notificationId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Notificat__accou__5EBF139D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A43883E9A");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}