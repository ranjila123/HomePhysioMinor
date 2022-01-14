using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedPhysioCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysioImage_ImageTypeModel_ImgID",
                table: "PhysioImage");

            migrationBuilder.DropTable(
                name: "PhysioTimeSlotsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PStatuModel",
                table: "PStatuModel");

            migrationBuilder.RenameTable(
                name: "PStatuModel",
                newName: "PStatusModel");

            migrationBuilder.RenameColumn(
                name: "ImgID",
                table: "PhysioImage",
                newName: "ImgId");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "PatientModel",
                newName: "Age");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PStatusModel",
                table: "PStatusModel",
                column: "PStatuCode");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysioImage_ImageTypeModel_ImgId",
                table: "PhysioImage",
                column: "ImgId",
                principalTable: "ImageTypeModel",
                principalColumn: "ImgId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysioImage_ImageTypeModel_ImgId",
                table: "PhysioImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PStatusModel",
                table: "PStatusModel");

            migrationBuilder.RenameTable(
                name: "PStatusModel",
                newName: "PStatuModel");

            migrationBuilder.RenameColumn(
                name: "ImgId",
                table: "PhysioImage",
                newName: "ImgID");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "PatientModel",
                newName: "age");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PStatuModel",
                table: "PStatuModel",
                column: "PStatuCode");

            migrationBuilder.CreateTable(
                name: "PhysioTimeSlotsModel",
                columns: table => new
                {
                    PhysioTimeSlotsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<int>(type: "int", nullable: false),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: false),
                    TimeShift = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysioTimeSlotsModel", x => x.PhysioTimeSlotsId);
                    table.ForeignKey(
                        name: "FK_PhysioTimeSlotsModel_PhysiotherapistModel_PhysiotherapistId",
                        column: x => x.PhysiotherapistId,
                        principalTable: "PhysiotherapistModel",
                        principalColumn: "PhysiotherapistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysioTimeSlotsModel_PhysiotherapistId",
                table: "PhysioTimeSlotsModel",
                column: "PhysiotherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysioImage_ImageTypeModel_ImgID",
                table: "PhysioImage",
                column: "ImgID",
                principalTable: "ImageTypeModel",
                principalColumn: "ImgId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
