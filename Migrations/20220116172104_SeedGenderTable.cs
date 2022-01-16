using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class SeedGenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into GenderModel(TypeName) values('Male')");
            migrationBuilder.Sql("insert into GenderModel(TypeName) values('Female')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
