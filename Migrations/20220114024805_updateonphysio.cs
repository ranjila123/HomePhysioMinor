using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class updateonphysio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_GenderModel_GenderDataGenderId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_GenderDataGenderId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropColumn(
                name: "GenderDataGenderId",
                table: "PhysiotherapistModel");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_GenderId",
                table: "PhysiotherapistModel",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_GenderModel_GenderId",
                table: "PhysiotherapistModel",
                column: "GenderId",
                principalTable: "GenderModel",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_GenderModel_GenderId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_GenderId",
                table: "PhysiotherapistModel");

            migrationBuilder.AddColumn<int>(
                name: "GenderDataGenderId",
                table: "PhysiotherapistModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_GenderDataGenderId",
                table: "PhysiotherapistModel",
                column: "GenderDataGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_GenderModel_GenderDataGenderId",
                table: "PhysiotherapistModel",
                column: "GenderDataGenderId",
                principalTable: "GenderModel",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
