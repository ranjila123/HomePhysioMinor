using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddAppointmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PhysioTimeSlotsModel");

            migrationBuilder.DropColumn(
                name: "TimeShift",
                table: "PhysioTimeSlotsModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeShift",
                table: "PhysioTimeSlotsModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AppointmentsModels",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsModels", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_AppointmentsModels_PatientModel_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientModel",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsModels_StatusModel_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "StatusModel",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModels_PatientId",
                table: "AppointmentsModels",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsModels_StatusCode",
                table: "AppointmentsModels",
                column: "StatusCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsModels");

            migrationBuilder.DropColumn(
                name: "DateTimeShift",
                table: "PhysioTimeSlotsModel");

            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "PhysioTimeSlotsModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeShift",
                table: "PhysioTimeSlotsModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
