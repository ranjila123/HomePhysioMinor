using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddAppointmentModelTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhysioTimeSlotsId",
                table: "AppointmentsModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModels_PhysioTimeSlotsId",
                table: "AppointmentsModels",
                column: "PhysioTimeSlotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsModels_PhysioTimeSlotsModel_PhysioTimeSlotsId",
                table: "AppointmentsModels",
                column: "PhysioTimeSlotsId",
                principalTable: "PhysioTimeSlotsModel",
                principalColumn: "PhysioTimeSlotsId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsModels_PhysioTimeSlotsModel_PhysioTimeSlotsId",
                table: "AppointmentsModels");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsModels_PhysioTimeSlotsId",
                table: "AppointmentsModels");

            migrationBuilder.DropColumn(
                name: "PhysioTimeSlotsId",
                table: "AppointmentsModels");
        }
    }
}
