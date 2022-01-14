using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddAppointmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentsModel",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysioTimeSlotsId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsModel", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_AppointmentsModel_PatientModel_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientModel",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsModel_PhysioTimeSlotsModel_PhysioTimeSlotsId",
                        column: x => x.PhysioTimeSlotsId,
                        principalTable: "PhysioTimeSlotsModel",
                        principalColumn: "PhysioTimeSlotsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsModel_StatusModel_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "StatusModel",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModel_PatientId",
                table: "AppointmentsModel",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModel_PhysioTimeSlotsId",
                table: "AppointmentsModel",
                column: "PhysioTimeSlotsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModel_StatusCode",
                table: "AppointmentsModel",
                column: "StatusCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsModel");
        }
    }
}
