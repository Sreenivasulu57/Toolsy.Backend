using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VSC.Toolsy.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Tool_Models_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ToolCategoryId",
                table: "Tool",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ToolAvailability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ToolId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolAvailability_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ToolCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IconUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolCategory_ToolCategory_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ToolCategory",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ToolSpecifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ToolId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolSpecifications_Tool_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateOfBirth", "DeletedAt", "DeletedBy", "Email", "EmailVerifiedAt", "FirstName", "Gender", "IsActive", "IsDeleted", "LastName", "PasswordHash", "PhoneNumber", "PhoneVerifiedAt", "ProfileImageUrl", "Roles", "Status", "UpdatedAt", "UpdatedBy", "VerificationStatus" },
                values: new object[,]
                {
                    { new Guid("a0b5d923-fd53-4a68-913b-7a6db1061e4d"), new DateTime(2025, 10, 21, 6, 6, 51, 749, DateTimeKind.Utc).AddTicks(1945), null, new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "admin1@example.com", new DateTime(2025, 10, 21, 11, 36, 52, 112, DateTimeKind.Local).AddTicks(8879), "Admin1", 0, true, false, "Admin1", "$2a$11$bkepdES2vp0IVtDkL.TjA.UIXlNZWbpeES0./sOwWuPMFLDo6JJfe", "1234567890", new DateTime(2025, 10, 21, 11, 36, 52, 112, DateTimeKind.Local).AddTicks(9334), "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin1", "[1]", 1, null, null, 1 },
                    { new Guid("b1c1e599-59c1-4b3d-b707-5aab9d3f38db"), new DateTime(2025, 10, 21, 6, 6, 52, 112, DateTimeKind.Utc).AddTicks(9630), null, new DateTime(1986, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "admin2@example.com", new DateTime(2025, 10, 21, 11, 36, 52, 259, DateTimeKind.Local).AddTicks(407), "Admin2", 1, true, false, "Admin2", "$2a$11$eKAPsEgHmz8xgwRs0uxYC.vu.xV04nwiPFcKKuASYLACgy9BuHt4e", "0987654321", new DateTime(2025, 10, 21, 11, 36, 52, 259, DateTimeKind.Local).AddTicks(416), "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin2", "[1]", 1, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tool_ToolCategoryId",
                table: "Tool",
                column: "ToolCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolAvailability_ToolId",
                table: "ToolAvailability",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolCategory_ParentCategoryId",
                table: "ToolCategory",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolSpecifications_ToolId",
                table: "ToolSpecifications",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tool_ToolCategory_ToolCategoryId",
                table: "Tool",
                column: "ToolCategoryId",
                principalTable: "ToolCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tool_ToolCategory_ToolCategoryId",
                table: "Tool");

            migrationBuilder.DropTable(
                name: "ToolAvailability");

            migrationBuilder.DropTable(
                name: "ToolCategory");

            migrationBuilder.DropTable(
                name: "ToolSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Tool_ToolCategoryId",
                table: "Tool");

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "Id",
                keyValue: new Guid("a0b5d923-fd53-4a68-913b-7a6db1061e4d"));

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "Id",
                keyValue: new Guid("b1c1e599-59c1-4b3d-b707-5aab9d3f38db"));

            migrationBuilder.DropColumn(
                name: "ToolCategoryId",
                table: "Tool");
        }
    }
}
