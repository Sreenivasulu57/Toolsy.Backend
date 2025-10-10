using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VSC.Toolsy.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RefresTokenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "Id",
                keyValue: new Guid("a0b5d923-fd53-4a68-913b-7a6db1061e4d"));

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "Id",
                keyValue: new Guid("b1c1e599-59c1-4b3d-b707-5aab9d3f38db"));

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ProfileId",
                table: "RefreshToken",
                column: "ProfileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateOfBirth", "DeletedAt", "DeletedBy", "Email", "EmailVerifiedAt", "FirstName", "Gender", "IsActive", "IsDeleted", "LastName", "PasswordHash", "PhoneNumber", "PhoneVerifiedAt", "ProfileImageUrl", "Roles", "Status", "UpdatedAt", "UpdatedBy", "VerificationStatus" },
                values: new object[,]
                {
                    { new Guid("a0b5d923-fd53-4a68-913b-7a6db1061e4d"), new DateTime(2025, 10, 9, 6, 9, 54, 504, DateTimeKind.Utc).AddTicks(1153), null, new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "admin1@example.com", new DateTime(2025, 10, 9, 11, 39, 54, 787, DateTimeKind.Local).AddTicks(1895), "Admin1", 0, true, false, "Admin1", "$2a$11$ipfB1vCXtjOGx/bJOcYbmuLrEolh8QAPIpfc5ezr8j47sFsszFQ5G", "1234567890", new DateTime(2025, 10, 9, 11, 39, 54, 787, DateTimeKind.Local).AddTicks(2290), "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin1", "[1]", 1, null, null, 1 },
                    { new Guid("b1c1e599-59c1-4b3d-b707-5aab9d3f38db"), new DateTime(2025, 10, 9, 6, 9, 54, 787, DateTimeKind.Utc).AddTicks(2677), null, new DateTime(1986, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "admin2@example.com", new DateTime(2025, 10, 9, 11, 39, 54, 933, DateTimeKind.Local).AddTicks(4783), "Admin2", 1, true, false, "Admin2", "$2a$11$uMJ7/M0RuOin2iPXFhWBpeGnapzivIYFHVl/rEVOSGVN/tNId3CG2", "0987654321", new DateTime(2025, 10, 9, 11, 39, 54, 933, DateTimeKind.Local).AddTicks(4796), "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin2", "[1]", 1, null, null, 1 }
                });
        }
    }
}
