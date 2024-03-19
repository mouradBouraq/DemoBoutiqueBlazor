using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBoutique.Infrastructure.Migrations
{
    public partial class initdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEK07SbeHjreWssD2+0ygvDn5OOJY9OTeIoVuZa427e7H9K3SaProJ75q64/nCtcAIA==", "3c9f4350-2c05-4182-be64-5499aecd1dc5" });

            migrationBuilder.InsertData(
                table: "Categorie",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(214), "mourad", "Clothes", null, "" },
                    { 2, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(256), "mourad", "Shoes", null, "" },
                    { 3, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(264), "mourad", "Pants", null, "" },
                    { 4, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(272), "mourad", "Shirts", null, "" }
                });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 1, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(291), "produit 1" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 2, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(300), "produit 2" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 3, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(307), "produit 3" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 4, new DateTime(2024, 3, 19, 23, 28, 36, 536, DateTimeKind.Local).AddTicks(315), "produit 4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEF2sfXNoL2NiD8rkYU3Hl1iTNV6dHWfO/cAZKBbfEAYmyXda0xHAOgalngq3fgaoYg==", "0aaae5ee-fcda-4068-916d-f8cbac7adb1c" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 9, new DateTime(2024, 3, 6, 23, 15, 56, 460, DateTimeKind.Local).AddTicks(8761), "test 9" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 9, new DateTime(2024, 3, 6, 23, 15, 56, 460, DateTimeKind.Local).AddTicks(8808), "test 2" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 11, new DateTime(2024, 3, 6, 23, 15, 56, 460, DateTimeKind.Local).AddTicks(8818), "test 3" });

            migrationBuilder.UpdateData(
                table: "Produit",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategorieId", "CreatedAt", "Libelle" },
                values: new object[] { 11, new DateTime(2024, 3, 6, 23, 15, 56, 460, DateTimeKind.Local).AddTicks(8826), "test 4" });
        }
    }
}
