using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortha.Migrations
{
    /// <inheritdoc />
    public partial class m895 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 667, DateTimeKind.Utc).AddTicks(3441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 662, DateTimeKind.Utc).AddTicks(1615));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 667, DateTimeKind.Utc).AddTicks(3441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 19, 15, 17, 8, 662, DateTimeKind.Utc).AddTicks(1615),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
