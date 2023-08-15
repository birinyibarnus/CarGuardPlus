using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGuardPlus.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyForAlertMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "AlertMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "AlertMessages");
        }
    }
}
