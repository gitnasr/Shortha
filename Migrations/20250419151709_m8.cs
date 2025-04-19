using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortha.Migrations
{
    /// <inheritdoc />
    public partial class m8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 667, DateTimeKind.Utc).AddTicks(3441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 14, 40, 835, DateTimeKind.Utc).AddTicks(4117));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 662, DateTimeKind.Utc).AddTicks(1615),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 14, 40, 835, DateTimeKind.Utc).AddTicks(4117),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 667, DateTimeKind.Utc).AddTicks(3441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 662, DateTimeKind.Utc).AddTicks(1615));
        }
    }
}
