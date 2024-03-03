using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBoutique.Infrastructure.Migrations
{
    public partial class addadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d", "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d", "SuperAdmin", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d", 0, "a194464e-3a04-48c2-b80f-5ffbbeb9d795", "system@gbam.com", true, false, null, "system@gbam.com", "system@gbam.com", "AQAAAAEAACcQAAAAEKmacYEUH9911IpDBVPCdX0vUsLU/JB5sxDoccKJbgR/hx/mhrPYrs9zW5ZNf1rXGg==", null, false, "2555319b-93e5-4892-a481-b29d8894362e", false, "system@gbam.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d", "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d", "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d");
        }
    }
}
