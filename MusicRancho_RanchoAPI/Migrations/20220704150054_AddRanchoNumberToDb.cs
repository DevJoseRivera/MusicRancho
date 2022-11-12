using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRancho_RanchoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRanchoNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RanchoNumbers",
                columns: table => new
                {
                    RanchoNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RanchoNumbers", x => x.RanchoNo);
                });

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8272));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8274));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RanchoNumbers");

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 28, 11, 31, 45, 726, DateTimeKind.Local).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 28, 11, 31, 45, 726, DateTimeKind.Local).AddTicks(6048));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 28, 11, 31, 45, 726, DateTimeKind.Local).AddTicks(6051));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 28, 11, 31, 45, 726, DateTimeKind.Local).AddTicks(6055));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 28, 11, 31, 45, 726, DateTimeKind.Local).AddTicks(6057));
        }
    }
}
