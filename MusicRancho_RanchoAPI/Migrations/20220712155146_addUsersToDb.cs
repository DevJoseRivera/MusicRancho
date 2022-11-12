using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRancho_RanchoAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUsersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 12, 10, 51, 46, 107, DateTimeKind.Local).AddTicks(7513), "https://someWebSite.com/blueranchoimages/rancho3.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 12, 10, 51, 46, 107, DateTimeKind.Local).AddTicks(7557), "https://someWebSite.com/blueranchoimages/rancho1.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 12, 10, 51, 46, 107, DateTimeKind.Local).AddTicks(7560), "https://someWebSite.com/blueranchoimages/rancho4.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 12, 10, 51, 46, 107, DateTimeKind.Local).AddTicks(7562), "https://someWebSite.com/blueranchoimages/rancho5.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 12, 10, 51, 46, 107, DateTimeKind.Local).AddTicks(7564), "https://someWebSite.com/blueranchoimages/rancho2.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 8, 14, 26, 58, 968, DateTimeKind.Local).AddTicks(4984), "https://someWebSiteimages.blob.core.windows.net/blueranchoimages/rancho3.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 8, 14, 26, 58, 968, DateTimeKind.Local).AddTicks(5072), "https://someWebSiteimages.blob.core.windows.net/blueranchoimages/rancho1.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 8, 14, 26, 58, 968, DateTimeKind.Local).AddTicks(5075), "https://someWebSiteimages.blob.core.windows.net/blueranchoimages/rancho4.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 8, 14, 26, 58, 968, DateTimeKind.Local).AddTicks(5077), "https://someWebSiteimages.blob.core.windows.net/blueranchoimages/rancho5.jpg" });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2022, 7, 8, 14, 26, 58, 968, DateTimeKind.Local).AddTicks(5079), "https://someWebSiteimages.blob.core.windows.net/blueranchoimages/rancho2.jpg" });
        }
    }
}
