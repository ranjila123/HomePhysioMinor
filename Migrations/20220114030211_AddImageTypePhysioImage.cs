using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddImageTypePhysioImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientModel_GenderModel_GenderDataGenderId",
                table: "PatientModel");

            migrationBuilder.DropIndex(
                name: "IX_PatientModel_GenderDataGenderId",
                table: "PatientModel");

            migrationBuilder.DropColumn(
                name: "GenderDataGenderId",
                table: "PatientModel");

            migrationBuilder.CreateTable(
                name: "ImageTypeModel",
                columns: table => new
                {
                    ImgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imgtype = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTypeModel", x => x.ImgId);
                });

            migrationBuilder.CreateTable(
                name: "PhysioImage",
                columns: table => new
                {
                    ImgID = table.Column<int>(type: "int", nullable: false),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysioImage", x => x.ImgID);
                    table.ForeignKey(
                        name: "FK_PhysioImage_ImageTypeModel_ImgID",
                        column: x => x.ImgID,
                        principalTable: "ImageTypeModel",
                        principalColumn: "ImgId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysioImage_PhysiotherapistModel_PhysiotherapistId",
                        column: x => x.PhysiotherapistId,
                        principalTable: "PhysiotherapistModel",
                        principalColumn: "PhysiotherapistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientModel_GenderId",
                table: "PatientModel",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysioImage_PhysiotherapistId",
                table: "PhysioImage",
                column: "PhysiotherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientModel_GenderModel_GenderId",
                table: "PatientModel",
                column: "GenderId",
                principalTable: "GenderModel",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientModel_GenderModel_GenderId",
                table: "PatientModel");

            migrationBuilder.DropTable(
                name: "PhysioImage");

            migrationBuilder.DropTable(
                name: "ImageTypeModel");

            migrationBuilder.DropIndex(
                name: "IX_PatientModel_GenderId",
                table: "PatientModel");

            migrationBuilder.AddColumn<int>(
                name: "GenderDataGenderId",
                table: "PatientModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientModel_GenderDataGenderId",
                table: "PatientModel",
                column: "GenderDataGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientModel_GenderModel_GenderDataGenderId",
                table: "PatientModel",
                column: "GenderDataGenderId",
                principalTable: "GenderModel",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
