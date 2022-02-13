using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class SeedingForStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into StatusModel(StatusCode,StatusType) values('1','Approve')");
            migrationBuilder.Sql("insert into StatusModel(StatusCode,StatusType) values('2','Pending')");
            migrationBuilder.Sql("insert into StatusModel(StatusCode,StatusType) values('3','Cancel')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
