using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRancho_RanchoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToRanchoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RanchoID",
                table: "RanchoNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9630));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9681));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9684));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9686));

            migrationBuilder.UpdateData(
                table: "Ranchos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9689));

            migrationBuilder.CreateIndex(
                name: "IX_RanchoNumbers_RanchoID",
                table: "RanchoNumbers",
                column: "RanchoID");

            migrationBuilder.AddForeignKey(
                name: "FK_RanchoNumbers_Ranchos_RanchoID",
                table: "RanchoNumbers",
                column: "RanchoID",
                principalTable: "Ranchos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RanchoNumbers_Ranchos_RanchoID",
                table: "RanchoNumbers");

            migrationBuilder.DropIndex(
                name: "IX_RanchoNumbers_RanchoID",
                table: "RanchoNumbers");

            migrationBuilder.DropColumn(
                name: "RanchoID",
                table: "RanchoNumbers");

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
    }
}
