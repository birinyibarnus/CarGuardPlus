using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGuardPlus.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licences",
                columns: table => new
                {
                    LicenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicencePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licences", x => x.LicenceId);
                    table.ForeignKey(
                        name: "FK_Licences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertMessages",
                columns: table => new
                {
                    AlertMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LicenceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertMessages", x => x.AlertMessageId);
                    table.ForeignKey(
                        name: "FK_AlertMessages_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlertMessages_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlertMessages_Licences_LicenceId",
                        column: x => x.LicenceId,
                        principalTable: "Licences",
                        principalColumn: "LicenceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertMessages_ApplicationUserId",
                table: "AlertMessages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertMessages_LicenceId",
                table: "AlertMessages",
                column: "LicenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertMessages_SenderUserId",
                table: "AlertMessages",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Licences_UserId",
                table: "Licences",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertMessages");

            migrationBuilder.DropTable(
                name: "Licences");
        }
    }
}
