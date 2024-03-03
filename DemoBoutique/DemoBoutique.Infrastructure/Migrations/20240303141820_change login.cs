using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBoutique.Infrastructure.Migrations
{
    public partial class changelogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "system@boutique.com", "system@boutique.com", "system@boutique.com", "AQAAAAEAACcQAAAAEHxXTlehZNcEPqN/kKmxzXPol07cEJ80riN6jn+V7n8F3XefVyWhxc86smbJ1OeXOw==", "0831d53d-57b0-4c28-a4fc-8a62a58464d1", "system@boutique.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "system@gbam.com", "system@gbam.com", "system@gbam.com", "AQAAAAEAACcQAAAAEKmacYEUH9911IpDBVPCdX0vUsLU/JB5sxDoccKJbgR/hx/mhrPYrs9zW5ZNf1rXGg==", "2555319b-93e5-4892-a481-b29d8894362e", "system@gbam.com" });
        }
    }
}
