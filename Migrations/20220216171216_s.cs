using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTypeModel_PatientModel_PatientModelPatientId",
                table: "ImageTypeModel");

            migrationBuilder.DropIndex(
                name: "IX_ImageTypeModel_PatientModelPatientId",
                table: "ImageTypeModel");

            migrationBuilder.DropColumn(
                name: "PatientModelPatientId",
                table: "ImageTypeModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientModelPatientId",
                table: "ImageTypeModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypeModel_PatientModelPatientId",
                table: "ImageTypeModel",
                column: "PatientModelPatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTypeModel_PatientModel_PatientModelPatientId",
                table: "ImageTypeModel",
                column: "PatientModelPatientId",
                principalTable: "PatientModel",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
