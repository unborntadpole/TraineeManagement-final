using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class ProcessingJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessingJobs",
                columns: table => new
                {
                    CorrelationId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    ErrorSummary = table.Column<string>(type: "longtext", nullable: true),
                    Started = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Finished = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingJobs", x => x.CorrelationId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 6, 26, 13, 25, 21, 111, DateTimeKind.Utc).AddTicks(1637), new DateTime(2026, 6, 26, 13, 25, 21, 111, DateTimeKind.Utc).AddTicks(1787) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessingJobs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 6, 22, 8, 5, 2, 376, DateTimeKind.Utc).AddTicks(1006), new DateTime(2026, 6, 22, 8, 5, 2, 376, DateTimeKind.Utc).AddTicks(1241) });
        }
    }
}
