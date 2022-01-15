using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedPaymentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentsModel",
                columns: table => new
                {
                    PaymentsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DistanceAmount = table.Column<int>(type: "int", nullable: false),
                    PStatusCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsModel", x => x.PaymentsId);
                    table.ForeignKey(
                        name: "FK_PaymentsModel_AppointmentsModels_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "AppointmentsModels",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentsModel_PaymentTypeModel_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypeModel",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentsModel_PStatusModel_PStatusCode",
                        column: x => x.PStatusCode,
                        principalTable: "PStatusModel",
                        principalColumn: "PStatuCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsModel_AppointmentId",
                table: "PaymentsModel",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsModel_PaymentTypeId",
                table: "PaymentsModel",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsModel_PStatusCode",
                table: "PaymentsModel",
                column: "PStatusCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentsModel");
        }
    }
}
