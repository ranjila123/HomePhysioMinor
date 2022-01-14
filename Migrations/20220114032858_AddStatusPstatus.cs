using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddStatusPstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PStatuModel",
                columns: table => new
                {
                    PStatuCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PStatusType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PStatuModel", x => x.PStatuCode);
                });

            migrationBuilder.CreateTable(
                name: "StatuModel",
                columns: table => new
                {
                    StatuCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatuModel", x => x.StatuCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PStatuModel");

            migrationBuilder.DropTable(
                name: "StatuModel");
        }
    }
}
