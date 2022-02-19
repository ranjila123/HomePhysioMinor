using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class seedingBookedInsatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into StatusModel(StatusCode,StatusType) values('4','Booked')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
