using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedPhysioCategoryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysioCategoryModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysioCategoryModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysioCategoryModel_CategoryModel_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryModel",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysioCategoryModel_PhysiotherapistModel_PhysiotherapistId",
                        column: x => x.PhysiotherapistId,
                        principalTable: "PhysiotherapistModel",
                        principalColumn: "PhysiotherapistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysioCategoryModel_CategoryId",
                table: "PhysioCategoryModel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysioCategoryModel_PhysiotherapistId",
                table: "PhysioCategoryModel",
                column: "PhysiotherapistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysioCategoryModel");
        }
    }
}
