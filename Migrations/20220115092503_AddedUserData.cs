using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PhysiotherapistModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PatientModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_UserId",
                table: "PhysiotherapistModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientModel_UserId",
                table: "PatientModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientModel_AspNetUsers_UserId",
                table: "PatientModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_AspNetUsers_UserId",
                table: "PhysiotherapistModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientModel_AspNetUsers_UserId",
                table: "PatientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_AspNetUsers_UserId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_UserId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PatientModel_UserId",
                table: "PatientModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PatientModel");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
