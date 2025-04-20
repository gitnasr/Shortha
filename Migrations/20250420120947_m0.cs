using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortha.Migrations
{
    /// <inheritdoc />
    public partial class m0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Device",
                table: "Visits",
                newName: "DeviceType");

            migrationBuilder.AddColumn<string>(
                name: "DeviceBrand",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceBrand",
                table: "Visits");

            migrationBuilder.RenameColumn(
                name: "DeviceType",
                table: "Visits",
                newName: "Device");
        }
    }
}
