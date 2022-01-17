using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class CAtegoryadded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('General Physiotherapist','Physiotherapist for all')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Pediactric Physiotherapist','Physiotherapist for children')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
