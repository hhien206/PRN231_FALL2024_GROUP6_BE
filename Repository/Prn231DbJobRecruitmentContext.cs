using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository;

public partial class Prn231DbJobRecruitmentContext : DbContext
{
    public Prn231DbJobRecruitmentContext()
    {
    }

    public Prn231DbJobRecruitmentContext(DbContextOptions<Prn231DbJobRecruitmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountSkillsMapping> AccountSkillsMappings { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<InterviewProcess> InterviewProcesses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<JobLevel> JobLevels { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<JobSkillsMapping> JobSkillsMappings { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5863F7D283D");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Accounts__Profil__4BAC3F29");
        });

        modelBuilder.Entity<AccountSkillsMapping>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.JobSkillId }).HasName("PK__AccountS__F5E1F3780530B9A9");

            entity.ToTable("AccountSkillsMapping");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.JobSkillId).HasColumnName("JobSkillID");
            entity.Property(e => e.Experienced)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.AccountSkillsMappings)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSk__Accou__628FA481");

            entity.HasOne(d => d.JobSkill).WithMany(p => p.AccountSkillsMappings)
                .HasForeignKey(d => d.JobSkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSk__JobSk__6383C8BA");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4F794E371612");

            entity.Property(e => e.ApplicationId)
                .ValueGeneratedNever()
                .HasColumnName("ApplicationID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Applications)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Applicati__Accou__6754599E");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Applicati__JobID__66603565");
        });

        modelBuilder.Entity<InterviewProcess>(entity =>
        {
            entity.HasKey(e => e.InterviewId).HasName("PK__Intervie__C97C583265CF0C56");

            entity.ToTable("InterviewProcess");

            entity.Property(e => e.InterviewId)
                .ValueGeneratedNever()
                .HasColumnName("InterviewID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.InterviewResult)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Interviewer)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Application).WithMany(p => p.InterviewProcesses)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__Interview__Appli__6A30C649");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Jobs__056690E2DC1779CC");

            entity.Property(e => e.JobId)
                .ValueGeneratedNever()
                .HasColumnName("JobID");
            entity.Property(e => e.ExperiencesRequired)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobBenefits).HasColumnType("text");
            entity.Property(e => e.JobCategoryId).HasColumnName("JobCategoryID");
            entity.Property(e => e.JobDescription).HasColumnType("text");
            entity.Property(e => e.JobLevelId).HasColumnName("JobLevelID");
            entity.Property(e => e.JobRequirements).HasColumnType("text");
            entity.Property(e => e.JobTime)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.RecruiterId).HasColumnName("RecruiterID");
            entity.Property(e => e.SalaryRange)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.JobCategory).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobCategoryId)
                .HasConstraintName("FK__Jobs__JobCategor__5AEE82B9");

            entity.HasOne(d => d.JobLevel).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobLevelId)
                .HasConstraintName("FK__Jobs__JobLevelID__5BE2A6F2");

            entity.HasOne(d => d.JobType).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobTypeId)
                .HasConstraintName("FK__Jobs__JobTypeID__59FA5E80");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("FK__Jobs__RecruiterI__59063A47");
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasKey(e => e.JobCategoryId).HasName("PK__JobCateg__302BAECD4921040C");

            entity.Property(e => e.JobCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("JobCategoryID");
            entity.Property(e => e.JobCategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobLevel>(entity =>
        {
            entity.HasKey(e => e.JobLevelId).HasName("PK__JobLevel__7594C84C80A0CDFC");

            entity.Property(e => e.JobLevelId)
                .ValueGeneratedNever()
                .HasColumnName("JobLevelID");
            entity.Property(e => e.JobLevelName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.JobSkillId).HasName("PK__JobSkill__17C56FED13D1D2C6");

            entity.Property(e => e.JobSkillId)
                .ValueGeneratedNever()
                .HasColumnName("JobSkillID");
            entity.Property(e => e.SkillName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobSkillsMapping>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.JobSkillId }).HasName("PK__JobSkill__C41AC61CE7321307");

            entity.ToTable("JobSkillsMapping");

            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.JobSkillId).HasColumnName("JobSkillID");
            entity.Property(e => e.ExperienceRequired)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Job).WithMany(p => p.JobSkillsMappings)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobSkills__JobID__5EBF139D");

            entity.HasOne(d => d.JobSkill).WithMany(p => p.JobSkillsMappings)
                .HasForeignKey(d => d.JobSkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobSkills__JobSk__5FB337D6");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.JobTypeId).HasName("PK__JobTypes__E1F4624DFBF4F63B");

            entity.Property(e => e.JobTypeId)
                .ValueGeneratedNever()
                .HasColumnName("JobTypeID");
            entity.Property(e => e.JobTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.ResumeId).HasName("PK__Resumes__D7D7A3175839B490");

            entity.Property(e => e.ResumeId)
                .ValueGeneratedNever()
                .HasColumnName("ResumeID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ResumeUrl)
                .HasMaxLength(255)
                .HasColumnName("ResumeURL");

            entity.HasOne(d => d.Account).WithMany(p => p.Resumes)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Resumes__Account__4E88ABD4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A3CEF08C6");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
        #region Seeding Data

        // Role Data
        modelBuilder.Entity<Role>().HasData(new Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = "Member"
        });
        modelBuilder.Entity<Role>().HasData(new Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = "Recruiter"
        });
        modelBuilder.Entity<Role>().HasData(new Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = "Admin"
        });

        // Image URLs
    //    string[] imageUrls = new string[]
    //    {
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F1.jpg?alt=media&token=edd76054-7ffd-4726-9f57-3a5abe125c10",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F2.jpg?alt=media&token=54f08c6f-e41e-4cde-b6c6-1e40194320a5",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F3.jpg?alt=media&token=f255817b-bc8f-405c-91f8-a5c733b20a16",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F4.jpg?alt=media&token=d87d662c-f2ff-4dec-ac0a-8da9fb426585",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F5.jpg?alt=media&token=d0f7a320-2176-4b9c-b6b3-df74cbf36baa",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2FDayChuyenBac.jpg?alt=media&token=4badf84f-4a3c-489a-bf3b-59bd3517dd8a",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F7.jpg?alt=media&token=5b91fa57-76a5-4911-820b-86f9b11ebbf9",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F8.jpg?alt=media&token=bbe17db6-2209-42ad-86dd-ee2422be3d56",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F9.jpg?alt=media&token=0f4b2e0c-ecf4-4ea8-9047-efe08d5b4e71",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F10.jpg?alt=media&token=55a92de8-ee5c-42c9-9abe-878a240dd0f1",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F11.jpg?alt=media&token=7afc835a-dc04-491f-a922-14f97c05ad47",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F12.jpg?alt=media&token=4d09d356-609b-410c-8a9a-bafdb4c906cd",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F12.jpg?alt=media&token=4d09d356-609b-410c-8a9a-bafdb4c906cd",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F14.jpg?alt=media&token=f9685ad2-52ff-4c5b-b27d-af7e82831126",
    //"https://firebasestorage.googleapis.com/v0/b/jss-prn221.appspot.com/o/Products%2F15.jpg?alt=media&token=54605a2e-243b-471a-95be-1636963e915a"
    //    };

#endregion
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
