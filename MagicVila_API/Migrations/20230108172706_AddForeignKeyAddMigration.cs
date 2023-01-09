using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilaVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyAddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 111,
                columns: new[] { "CreatedDate", "VillaID" },
                values: new object[] { new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(782), 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 122,
                columns: new[] { "CreatedDate", "VillaID" },
                values: new object[] { new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(783), 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 123,
                columns: new[] { "CreatedDate", "VillaID" },
                values: new object[] { new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(784), 0 });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 211,
                columns: new[] { "CreatedDate", "VillaID" },
                values: new object[] { new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(785), 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(648));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 8, 18, 27, 5, 930, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(7129));

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 211,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(6952));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(6957));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 7, 18, 51, 24, 827, DateTimeKind.Local).AddTicks(6959));
        }
    }
}
