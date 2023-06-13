using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkDevTask.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0574ef42-4f24-404b-8388-4633bd4a55fe");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7f37081b-4d1b-4204-89e0-a2847cea58a8");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b9bbdb3-88f0-4b85-bb10-50fb9548ce70", "0fc58de8-482a-48c2-9ccc-d7ab28f3d07b", "Admin", "ADMIN" },
                    { "e6a6c73d-5012-4272-82a8-cc66e29a70b9", "716af88e-e5d0-4fda-aaf9-16e4e776a3b4", "User", "USER" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e6a6c73d-5012-4272-82a8-cc66e29a70b9", "05d13987-53f8-4c7f-982c-8721f52d2b4e" },
                    { "7b9bbdb3-88f0-4b85-bb10-50fb9548ce70", "a2e30d98-0852-4f9e-8fb6-246f0ab360e4" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SkillsAsString", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "05d13987-53f8-4c7f-982c-8721f52d2b4e", 0, "ad4c4d06-2700-4066-8ae8-8824b58e2af7", new DateTime(2023, 6, 13, 3, 43, 46, 947, DateTimeKind.Local).AddTicks(608), "user@test.com", false, "user", "user", false, null, "USER@TEST.COM", "USER", "AQAAAAEAACcQAAAAEMNzFYjc2An2bBT31dGD2mN/QeLSKlsptaKPy4iptv3O8+Ycy5qzDktnFUCJNs9WjQ==", null, false, "59221de6-c0e9-49ae-9eac-5372c089d4eb", null, false, "user" },
                    { "a2e30d98-0852-4f9e-8fb6-246f0ab360e4", 0, "ffc64d6e-73b4-45e9-9918-4c2b1b28f2fa", new DateTime(2023, 6, 13, 3, 43, 46, 944, DateTimeKind.Local).AddTicks(2029), "admin@test.com", false, "admin", "admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAELqRkfVd8xyHLY2zZ5Xdy/GFF9Z9e83AtpZn+ph9tk80OAmGFSsQRR7/q70z0eG9qw==", null, false, "d8f1d17d-13a3-4b3f-9e3c-65073ebde16e", null, false, "admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7b9bbdb3-88f0-4b85-bb10-50fb9548ce70");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e6a6c73d-5012-4272-82a8-cc66e29a70b9");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e6a6c73d-5012-4272-82a8-cc66e29a70b9", "05d13987-53f8-4c7f-982c-8721f52d2b4e" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7b9bbdb3-88f0-4b85-bb10-50fb9548ce70", "a2e30d98-0852-4f9e-8fb6-246f0ab360e4" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Users",
                keyColumn: "Id",
                keyValue: "05d13987-53f8-4c7f-982c-8721f52d2b4e");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Users",
                keyColumn: "Id",
                keyValue: "a2e30d98-0852-4f9e-8fb6-246f0ab360e4");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0574ef42-4f24-404b-8388-4633bd4a55fe", "7cbb9a49-134a-4a6c-8177-c8d5514d837f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f37081b-4d1b-4204-89e0-a2847cea58a8", "3d32714e-4d4d-4a6e-bbbc-449803adbb62", "User", "USER" });
        }
    }
}
