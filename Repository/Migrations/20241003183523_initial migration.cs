using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    JobCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobCategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobCateg__302BAECD4921040C", x => x.JobCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "JobLevels",
                columns: table => new
                {
                    JobLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobLevelName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobLevel__7594C84C80A0CDFC", x => x.JobLevelID);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    JobSkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobSkill__17C56FED13D1D2C6", x => x.JobSkillID);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    JobTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobTypes__E1F4624DFBF4F63B", x => x.JobTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A3CEF08C6", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts__349DA5863F7D283D", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__Accounts__Profil__4BAC3F29",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "AccountSkillsMapping",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobSkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Experienced = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AccountS__F5E1F3780530B9A9", x => new { x.AccountID, x.JobSkillID });
                    table.ForeignKey(
                        name: "FK__AccountSk__Accou__628FA481",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__AccountSk__JobSk__6383C8BA",
                        column: x => x.JobSkillID,
                        principalTable: "JobSkills",
                        principalColumn: "JobSkillID");
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    JobDescription = table.Column<string>(type: "text", nullable: true),
                    JobRequirements = table.Column<string>(type: "text", nullable: true),
                    JobBenefits = table.Column<string>(type: "text", nullable: true),
                    JobTime = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SalaryRange = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ExperiencesRequired = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: true),
                    RecruiterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Jobs__056690E2DC1779CC", x => x.JobID);
                    table.ForeignKey(
                        name: "FK__Jobs__JobCategor__5AEE82B9",
                        column: x => x.JobCategoryID,
                        principalTable: "JobCategories",
                        principalColumn: "JobCategoryID");
                    table.ForeignKey(
                        name: "FK__Jobs__JobLevelID__5BE2A6F2",
                        column: x => x.JobLevelID,
                        principalTable: "JobLevels",
                        principalColumn: "JobLevelID");
                    table.ForeignKey(
                        name: "FK__Jobs__JobTypeID__59FA5E80",
                        column: x => x.JobTypeID,
                        principalTable: "JobTypes",
                        principalColumn: "JobTypeID");
                    table.ForeignKey(
                        name: "FK__Jobs__RecruiterI__59063A47",
                        column: x => x.RecruiterID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    ResumeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResumeURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resumes__D7D7A3175839B490", x => x.ResumeID);
                    table.ForeignKey(
                        name: "FK__Resumes__Account__4E88ABD4",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__C93A4F794E371612", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK__Applicati__Accou__6754599E",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Applicati__JobID__66603565",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "JobID");
                });

            migrationBuilder.CreateTable(
                name: "JobSkillsMapping",
                columns: table => new
                {
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobSkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceRequired = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobSkill__C41AC61CE7321307", x => new { x.JobID, x.JobSkillID });
                    table.ForeignKey(
                        name: "FK__JobSkills__JobID__5EBF139D",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "JobID");
                    table.ForeignKey(
                        name: "FK__JobSkills__JobSk__5FB337D6",
                        column: x => x.JobSkillID,
                        principalTable: "JobSkills",
                        principalColumn: "JobSkillID");
                });

            migrationBuilder.CreateTable(
                name: "InterviewProcess",
                columns: table => new
                {
                    InterviewID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InterviewDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Interviewer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    InterviewResult = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Intervie__C97C583265CF0C56", x => x.InterviewID);
                    table.ForeignKey(
                        name: "FK__Interview__Appli__6A30C649",
                        column: x => x.ApplicationID,
                        principalTable: "Applications",
                        principalColumn: "ApplicationID");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { new Guid("4a5dd2d6-de03-48cc-94c2-d8a6799b1825"), "Admin" },
                    { new Guid("5a7dd16e-5af3-4f1f-8cf5-f1f22c57b633"), "Member" },
                    { new Guid("b8ddd40d-8e2c-4c1c-96e1-79bf6a3aad88"), "Recruiter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSkillsMapping_JobSkillID",
                table: "AccountSkillsMapping",
                column: "JobSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AccountID",
                table: "Applications",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobID",
                table: "Applications",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewProcess_ApplicationID",
                table: "InterviewProcess",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobCategoryID",
                table: "Jobs",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobLevelID",
                table: "Jobs",
                column: "JobLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeID",
                table: "Jobs",
                column: "JobTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RecruiterID",
                table: "Jobs",
                column: "RecruiterID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkillsMapping_JobSkillID",
                table: "JobSkillsMapping",
                column: "JobSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AccountID",
                table: "Resumes",
                column: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSkillsMapping");

            migrationBuilder.DropTable(
                name: "InterviewProcess");

            migrationBuilder.DropTable(
                name: "JobSkillsMapping");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "JobLevels");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
