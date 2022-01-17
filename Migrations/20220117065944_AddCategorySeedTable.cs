using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddCategorySeedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CitizenshipNumber",
                table: "PhysiotherapistModel",
                type: "nvarchar(max)",
                nullable: true);
           
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('General Physiotherapist','Physiotherapist for all')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Pediactric Physiotherapist','Physiotherapist for children')");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CitizenshipNumber",
                table: "PhysiotherapistModel");
        }

    }
}
