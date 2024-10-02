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

    public virtual DbSet<AccountJobkill> AccountJobkills { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<Hr> Hrs { get; set; }

    public virtual DbSet<InterviewProcess> InterviewProcesses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<JobJobSkill> JobJobSkills { get; set; }

    public virtual DbSet<JobLevel> JobLevels { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Jobkill> Jobkills { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__F267251EFB8AF94F");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.AccessToken)
                .HasMaxLength(255)
                .HasColumnName("accessToken");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("dateCreate");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__roleId__6477ECF3");
        });

        modelBuilder.Entity<AccountJobkill>(entity =>
        {
            entity.HasKey(e => e.AccountJobkillId).HasName("PK__AccountJ__89B74EC7D5118377");

            entity.ToTable("AccountJobkill");

            entity.Property(e => e.AccountJobkillId).HasColumnName("accountJobkillId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Experience)
                .HasMaxLength(100)
                .HasColumnName("experience");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountJobkills)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__AccountJo__accou__656C112C");

            entity.HasOne(d => d.Jobkill).WithMany(p => p.AccountJobkills)
                .HasForeignKey(d => d.JobkillId)
                .HasConstraintName("FK__AccountJo__Jobki__66603565");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__79FDB1CF69C6E7AA");

            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId).HasColumnName("applicationId");
            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime")
                .HasColumnName("applicationDate");
            entity.Property(e => e.CvId).HasColumnName("cvId");
            entity.Property(e => e.JobId).HasColumnName("jobId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Cv).WithMany(p => p.Applications)
                .HasForeignKey(d => d.CvId)
                .HasConstraintName("FK__Applicatio__cvId__68487DD7");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Applicati__jobId__6754599E");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__A15CBEAE20D109D5");

            entity.ToTable("Certificate");

            entity.Property(e => e.CertificateId).HasColumnName("certificateId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CertificateUrl)
                .HasMaxLength(255)
                .HasColumnName("certificateUrl");

            entity.HasOne(d => d.Account).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Certifica__accou__693CA210");
        });

        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CV__3213E83F24376A23");

            entity.ToTable("CV");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UrlCv)
                .HasMaxLength(255)
                .HasColumnName("urlCV");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Cvs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CV__userId__6A30C649");
        });

        modelBuilder.Entity<Hr>(entity =>
        {
            entity.HasKey(e => e.HrId).HasName("PK__HR__7FF1EBB105506AB5");

            entity.ToTable("HR");

            entity.Property(e => e.HrId).HasColumnName("hrId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(255)
                .HasColumnName("position");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Hrs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__HR__accountId__6B24EA82");
        });

        modelBuilder.Entity<InterviewProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__Intervie__01C9EB22DE08F5C5");

            entity.ToTable("InterviewProcess");

            entity.Property(e => e.ProcessId).HasColumnName("processId");
            entity.Property(e => e.ApplicantId).HasColumnName("applicantId");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .HasColumnName("details");
            entity.Property(e => e.RecruiterId).HasColumnName("recruiterId");
            entity.Property(e => e.RoundNumber).HasColumnName("roundNumber");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Applicant).WithMany(p => p.InterviewProcesses)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK__Interview__appli__6C190EBB");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.InterviewProcesses)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("FK__Interview__recru__6D0D32F4");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__164AA1A850119BF8");

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
            entity.Property(e => e.JobTime)
                .HasMaxLength(100)
                .HasColumnName("jobTime");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("jobTitle");
            entity.Property(e => e.Requirement)
                .HasMaxLength(500)
                .HasColumnName("requirement");
            entity.Property(e => e.SalaryRange)
                .HasMaxLength(100)
                .HasColumnName("salaryRange");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Job__accountId__6E01572D");

            entity.HasOne(d => d.JobCategory).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobCategoryId)
                .HasConstraintName("FK__Job__JobCategory__6EF57B66");

            entity.HasOne(d => d.JobLevel).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobLevelId)
                .HasConstraintName("FK__Job__JobLevelId__6FE99F9F");

            entity.HasOne(d => d.JobType).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobTypeId)
                .HasConstraintName("FK__Job__JobTypeId__70DDC3D8");
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobCateg__3214EC07D24F531B");

            entity.ToTable("JobCategory");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<JobJobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobJobSk__3214EC07EDBA9239");

            entity.ToTable("JobJobSkill");

            entity.HasOne(d => d.Job).WithMany(p => p.JobJobSkills)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__JobJobSki__JobId__71D1E811");

            entity.HasOne(d => d.JobSkill).WithMany(p => p.JobJobSkills)
                .HasForeignKey(d => d.JobSkillId)
                .HasConstraintName("FK__JobJobSki__JobSk__72C60C4A");
        });

        modelBuilder.Entity<JobLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobLevel__3214EC07E871CF45");

            entity.ToTable("JobLevel");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobType__3214EC07192F78AD");

            entity.ToTable("JobType");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Jobkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Jobkill__3214EC0770D798D4");

            entity.ToTable("Jobkill");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462ACBA4288F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFF72F740CF");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("dateCreate");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");

            entity.HasOne(d => d.Account).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__User__accountId__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}