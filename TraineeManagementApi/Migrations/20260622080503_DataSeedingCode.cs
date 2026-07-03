using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LearningTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ExpectedTechStack = table.Column<string>(type: "longtext", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTasks", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Expertise = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    TechStack = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Username = table.Column<string>(type: "longtext", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Remarks = table.Column<string>(type: "longtext", nullable: true),
                    TraineeId = table.Column<long>(type: "bigint", nullable: false),
                    MentorId = table.Column<long>(type: "bigint", nullable: false),
                    LearningTaskId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_LearningTasks_LearningTaskId",
                        column: x => x.LearningTaskId,
                        principalTable: "LearningTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    SubmissionUrl = table.Column<string>(type: "longtext", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TaskAssignmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_TaskAssignments_TaskAssignmentId",
                        column: x => x.TaskAssignmentId,
                        principalTable: "TaskAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReviewStatus = table.Column<string>(type: "longtext", nullable: false),
                    Feedback = table.Column<string>(type: "longtext", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MentorId = table.Column<long>(type: "bigint", nullable: false),
                    SubmissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubmissionFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OriginalFileName = table.Column<string>(type: "longtext", nullable: false),
                    GeneratedStorageName = table.Column<string>(type: "longtext", nullable: false),
                    ContentType = table.Column<string>(type: "longtext", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Checksum = table.Column<string>(type: "longtext", nullable: false),
                    UploadedByUser = table.Column<string>(type: "longtext", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SubmissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionFiles_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "LearningTasks",
                columns: new[] { "Id", "CreatedDate", "Description", "DueDate", "ExpectedTechStack", "Status", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 17, 6, 56, 8, 624, DateTimeKind.Unspecified).AddTicks(3950), "Build a task tracker to creat", new DateTime(2026, 6, 20, 6, 54, 2, 743, DateTimeKind.Unspecified), "manage tasks", "Published", "Task Tracker", new DateTime(2026, 6, 17, 6, 56, 8, 624, DateTimeKind.Unspecified).AddTicks(3960) },
                    { 2L, new DateTime(2026, 6, 17, 6, 57, 37, 782, DateTimeKind.Unspecified).AddTicks(6670), "Build an API server with inMemory database", new DateTime(2026, 6, 23, 6, 54, 2, 743, DateTimeKind.Unspecified), "ASP.NET Core WebAPI", "Published", "Trainee Management API - Phase 1", new DateTime(2026, 6, 17, 6, 57, 37, 782, DateTimeKind.Unspecified).AddTicks(6680) },
                    { 3L, new DateTime(2026, 6, 17, 6, 57, 58, 632, DateTimeKind.Unspecified).AddTicks(1110), "Build an API server with sql database", new DateTime(2026, 6, 28, 6, 54, 2, 743, DateTimeKind.Unspecified), "ASP.NET Core WebAPI", "Published", "Trainee Management API - Phase 2", new DateTime(2026, 6, 17, 6, 57, 58, 632, DateTimeKind.Unspecified).AddTicks(1120) }
                });

            migrationBuilder.InsertData(
                table: "Mentors",
                columns: new[] { "Id", "CreatedDate", "Email", "Expertise", "FirstName", "LastName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 17, 6, 50, 9, 571, DateTimeKind.Unspecified).AddTicks(4110), "abhay.gori@zeuslearning.com", "ASP .Net Core", "Abhay", "Gori", "Active", new DateTime(2026, 6, 17, 6, 50, 9, 571, DateTimeKind.Unspecified).AddTicks(4120) },
                    { 2L, new DateTime(2026, 6, 17, 6, 50, 31, 77, DateTimeKind.Unspecified).AddTicks(2600), "mentor2@zeuslearning.com", "ASP .Net Core", "Rheetik", "Sharma", "Active", new DateTime(2026, 6, 17, 6, 50, 31, 77, DateTimeKind.Unspecified).AddTicks(2610) },
                    { 3L, new DateTime(2026, 6, 17, 6, 50, 50, 967, DateTimeKind.Unspecified).AddTicks(2200), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 50, 50, 967, DateTimeKind.Unspecified).AddTicks(2210) },
                    { 4L, new DateTime(2026, 6, 17, 6, 51, 16, 940, DateTimeKind.Unspecified).AddTicks(2100), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 16, 940, DateTimeKind.Unspecified).AddTicks(2110) },
                    { 5L, new DateTime(2026, 6, 17, 6, 51, 17, 105, DateTimeKind.Unspecified).AddTicks(920), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 105, DateTimeKind.Unspecified).AddTicks(930) },
                    { 6L, new DateTime(2026, 6, 17, 6, 51, 17, 284, DateTimeKind.Unspecified).AddTicks(3300), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 284, DateTimeKind.Unspecified).AddTicks(3300) },
                    { 7L, new DateTime(2026, 6, 17, 6, 51, 17, 453, DateTimeKind.Unspecified).AddTicks(3010), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 453, DateTimeKind.Unspecified).AddTicks(3020) },
                    { 8L, new DateTime(2026, 6, 17, 6, 51, 17, 624, DateTimeKind.Unspecified).AddTicks(1720), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 624, DateTimeKind.Unspecified).AddTicks(1730) },
                    { 9L, new DateTime(2026, 6, 17, 6, 51, 17, 775, DateTimeKind.Unspecified).AddTicks(7100), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 775, DateTimeKind.Unspecified).AddTicks(7110) },
                    { 10L, new DateTime(2026, 6, 17, 6, 51, 17, 931, DateTimeKind.Unspecified).AddTicks(520), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 17, 931, DateTimeKind.Unspecified).AddTicks(520) },
                    { 11L, new DateTime(2026, 6, 17, 6, 51, 18, 83, DateTimeKind.Unspecified).AddTicks(6480), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 18, 83, DateTimeKind.Unspecified).AddTicks(6490) },
                    { 12L, new DateTime(2026, 6, 17, 6, 51, 18, 297, DateTimeKind.Unspecified).AddTicks(7400), "mentor3@zeuslearning.com", "ASP .Net Core", "Jay Prakash", "Yadav", "Active", new DateTime(2026, 6, 17, 6, 51, 18, 297, DateTimeKind.Unspecified).AddTicks(7410) }
                });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "Status", "TechStack", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 17, 6, 48, 26, 726, DateTimeKind.Unspecified).AddTicks(420), "trainee1@example.com", "Samriddh", "Singh", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 48, 26, 726, DateTimeKind.Unspecified).AddTicks(430) },
                    { 2L, new DateTime(2026, 6, 17, 6, 48, 42, 651, DateTimeKind.Unspecified).AddTicks(1340), "trainee2@example.com", "Ankit", "Kumar", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 48, 42, 651, DateTimeKind.Unspecified).AddTicks(1350) },
                    { 3L, new DateTime(2026, 6, 17, 6, 48, 56, 664, DateTimeKind.Unspecified).AddTicks(5990), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 48, 56, 664, DateTimeKind.Unspecified).AddTicks(6000) },
                    { 4L, new DateTime(2026, 6, 17, 6, 49, 0, 694, DateTimeKind.Unspecified).AddTicks(4270), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 0, 694, DateTimeKind.Unspecified).AddTicks(4280) },
                    { 5L, new DateTime(2026, 6, 17, 6, 49, 0, 858, DateTimeKind.Unspecified).AddTicks(2750), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 0, 858, DateTimeKind.Unspecified).AddTicks(2760) },
                    { 6L, new DateTime(2026, 6, 17, 6, 49, 1, 14, DateTimeKind.Unspecified).AddTicks(6140), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 14, DateTimeKind.Unspecified).AddTicks(6150) },
                    { 7L, new DateTime(2026, 6, 17, 6, 49, 1, 146, DateTimeKind.Unspecified).AddTicks(7410), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 146, DateTimeKind.Unspecified).AddTicks(7420) },
                    { 8L, new DateTime(2026, 6, 17, 6, 49, 1, 320, DateTimeKind.Unspecified).AddTicks(8370), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 320, DateTimeKind.Unspecified).AddTicks(8380) },
                    { 9L, new DateTime(2026, 6, 17, 6, 49, 1, 484, DateTimeKind.Unspecified).AddTicks(3390), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 484, DateTimeKind.Unspecified).AddTicks(3390) },
                    { 10L, new DateTime(2026, 6, 17, 6, 49, 1, 654, DateTimeKind.Unspecified).AddTicks(5260), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 654, DateTimeKind.Unspecified).AddTicks(5270) },
                    { 11L, new DateTime(2026, 6, 17, 6, 49, 1, 815, DateTimeKind.Unspecified).AddTicks(7250), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 815, DateTimeKind.Unspecified).AddTicks(7260) },
                    { 12L, new DateTime(2026, 6, 17, 6, 49, 1, 974, DateTimeKind.Unspecified).AddTicks(660), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 1, 974, DateTimeKind.Unspecified).AddTicks(670) },
                    { 13L, new DateTime(2026, 6, 17, 6, 49, 2, 151, DateTimeKind.Unspecified).AddTicks(1600), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 2, 151, DateTimeKind.Unspecified).AddTicks(1610) },
                    { 14L, new DateTime(2026, 6, 17, 6, 49, 2, 295, DateTimeKind.Unspecified).AddTicks(3860), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 2, 295, DateTimeKind.Unspecified).AddTicks(3870) },
                    { 15L, new DateTime(2026, 6, 17, 6, 49, 2, 449, DateTimeKind.Unspecified).AddTicks(9120), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 2, 449, DateTimeKind.Unspecified).AddTicks(9130) },
                    { 16L, new DateTime(2026, 6, 17, 6, 49, 2, 608, DateTimeKind.Unspecified).AddTicks(7870), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 2, 608, DateTimeKind.Unspecified).AddTicks(7880) },
                    { 17L, new DateTime(2026, 6, 17, 6, 49, 2, 830, DateTimeKind.Unspecified).AddTicks(6150), "trainee3@example.com", "Yash", "Sharma", "Active", "ASP.Net core", new DateTime(2026, 6, 17, 6, 49, 2, 830, DateTimeKind.Unspecified).AddTicks(6160) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "PasswordHash", "Role", "UpdatedDate", "Username" },
                values: new object[] { 1L, new DateTime(2026, 6, 22, 8, 5, 2, 376, DateTimeKind.Utc).AddTicks(1006), "samriddh.singh@zeuslearning.com", "AQAAAAIAAYagAAAAEP+QfNdJZtmZSCQUsvRTWt8NlKADYbY44q8GjYNIUhVn8c2ANxKiw50h4muvwf7ydg==", "Admin", new DateTime(2026, 6, 22, 8, 5, 2, 376, DateTimeKind.Utc).AddTicks(1241), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MentorId",
                table: "Reviews",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SubmissionId",
                table: "Reviews",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_SubmissionId",
                table: "SubmissionFiles",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_TaskAssignmentId",
                table: "Submissions",
                column: "TaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_LearningTaskId",
                table: "TaskAssignments",
                column: "LearningTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_MentorId",
                table: "TaskAssignments",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_TraineeId",
                table: "TaskAssignments",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SubmissionFiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropTable(
                name: "LearningTasks");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "Trainees");
        }
    }
}
