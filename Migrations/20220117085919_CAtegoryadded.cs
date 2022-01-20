using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class CAtegoryadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_CategoryModel_CategoryId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_CategoryId",
                table: "PhysiotherapistModel");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PhysiotherapistModel");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PhysiotherapistModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_CategoryId",
                table: "PhysiotherapistModel",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_CategoryModel_CategoryId",
                table: "PhysiotherapistModel",
                column: "CategoryId",
                principalTable: "CategoryModel",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
