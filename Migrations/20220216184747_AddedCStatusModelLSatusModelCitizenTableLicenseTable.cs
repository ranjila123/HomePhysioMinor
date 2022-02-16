using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddedCStatusModelLSatusModelCitizenTableLicenseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CStatusModel",
                table: "PhysiotherapistModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LStatusModel",
                table: "PhysiotherapistModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CitizenshipTable",
                columns: table => new
                {
                    CitizenShipNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenshipTable", x => x.CitizenShipNumber);
                });

            migrationBuilder.CreateTable(
                name: "CStatusModel",
                columns: table => new
                {
                    CStatusModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CStatusModel", x => x.CStatusModelId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseTable",
                columns: table => new
                {
                    LicenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTable", x => x.LicenseNumber);
                });

            migrationBuilder.CreateTable(
                name: "LStatusModel",
                columns: table => new
                {
                    LStatusModelid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LStatusModel", x => x.LStatusModelid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_CStatusModel",
                table: "PhysiotherapistModel",
                column: "CStatusModel");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapistModel_LStatusModel",
                table: "PhysiotherapistModel",
                column: "LStatusModel");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_CStatusModel_CStatusModel",
                table: "PhysiotherapistModel",
                column: "CStatusModel",
                principalTable: "CStatusModel",
                principalColumn: "CStatusModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysiotherapistModel_LStatusModel_LStatusModel",
                table: "PhysiotherapistModel",
                column: "LStatusModel",
                principalTable: "LStatusModel",
                principalColumn: "LStatusModelid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_CStatusModel_CStatusModel",
                table: "PhysiotherapistModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysiotherapistModel_LStatusModel_LStatusModel",
                table: "PhysiotherapistModel");

            migrationBuilder.DropTable(
                name: "CitizenshipTable");

            migrationBuilder.DropTable(
                name: "CStatusModel");

            migrationBuilder.DropTable(
                name: "LicenseTable");

            migrationBuilder.DropTable(
                name: "LStatusModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_CStatusModel",
                table: "PhysiotherapistModel");

            migrationBuilder.DropIndex(
                name: "IX_PhysiotherapistModel_LStatusModel",
                table: "PhysiotherapistModel");

            migrationBuilder.DropColumn(
                name: "CStatusModel",
                table: "PhysiotherapistModel");

            migrationBuilder.DropColumn(
                name: "LStatusModel",
                table: "PhysiotherapistModel");
        }
    }
}
