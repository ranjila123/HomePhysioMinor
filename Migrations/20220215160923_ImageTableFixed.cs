using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class ImageTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into GenderModel(TypeName) values('Others')");
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhysioImage",
                table: "PhysioImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientImage",
                table: "PatientImage");

            migrationBuilder.AddColumn<int>(
                name: "PhysioImageId",
                table: "PhysioImage",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PatientImageId",
                table: "PatientImage",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PatientModelPatientId",
                table: "ImageTypeModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhysioImage",
                table: "PhysioImage",
                column: "PhysioImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientImage",
                table: "PatientImage",
                column: "PatientImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysioImage_ImgId",
                table: "PhysioImage",
                column: "ImgId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientImage_ImgId",
                table: "PatientImage",
                column: "ImgId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTypeModel_PatientModel_PatientModelPatientId",
                table: "ImageTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhysioImage",
                table: "PhysioImage");

            migrationBuilder.DropIndex(
                name: "IX_PhysioImage_ImgId",
                table: "PhysioImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientImage",
                table: "PatientImage");

            migrationBuilder.DropIndex(
                name: "IX_PatientImage_ImgId",
                table: "PatientImage");

            migrationBuilder.DropIndex(
                name: "IX_ImageTypeModel_PatientModelPatientId",
                table: "ImageTypeModel");

            migrationBuilder.DropColumn(
                name: "PhysioImageId",
                table: "PhysioImage");

            migrationBuilder.DropColumn(
                name: "PatientImageId",
                table: "PatientImage");

            migrationBuilder.DropColumn(
                name: "PatientModelPatientId",
                table: "ImageTypeModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhysioImage",
                table: "PhysioImage",
                column: "ImgId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientImage",
                table: "PatientImage",
                column: "ImgId");
        }
    }
}
