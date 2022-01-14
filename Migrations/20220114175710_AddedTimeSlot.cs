using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysioTimeSlotsModel",
                columns: table => new
                {
                    PhysioTimeSlotsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<int>(type: "int", nullable: false),
                    TimeShift = table.Column<int>(type: "int", nullable: false),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysioTimeSlotsModel");
        }
    }
}
