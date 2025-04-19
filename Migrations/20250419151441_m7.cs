using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortha.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 14, 40, 835, DateTimeKind.Utc).AddTicks(4117),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 12, 23, 52, 685, DateTimeKind.Utc).AddTicks(4595));

            migrationBuilder.AddColumn<bool>(
                name: "isBlocked",
                table: "Urls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isCustom",
                table: "Urls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Urls",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "isCustom",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Urls");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 12, 23, 52, 685, DateTimeKind.Utc).AddTicks(4595),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 14, 40, 835, DateTimeKind.Utc).AddTicks(4117));
        }
    }
}
