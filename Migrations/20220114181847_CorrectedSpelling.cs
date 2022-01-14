using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class CorrectedSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatuModel",
                table: "StatuModel");

            migrationBuilder.RenameTable(
                name: "StatuModel",
                newName: "StatusModel");

            migrationBuilder.RenameColumn(
                name: "StatuCode",
                table: "StatusModel",
                newName: "StatusCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusModel",
                table: "StatusModel",
                column: "StatusCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusModel",
                table: "StatusModel");

            migrationBuilder.RenameTable(
                name: "StatusModel",
                newName: "StatuModel");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "StatuModel",
                newName: "StatuCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatuModel",
                table: "StatuModel",
                column: "StatuCode");
        }
    }
}
