using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class AddImageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into ImageTypeModel(ImgType) values('ProfilePhoto')");
            migrationBuilder.Sql("insert into ImageTypeModel(ImgType) values('CitizenshipFront')");
            migrationBuilder.Sql("insert into ImageTypeModel(ImgType) values('CitizenshipBack')");
            migrationBuilder.Sql("insert into ImageTypeModel(ImgType) values('LicenseFront')");
            migrationBuilder.Sql("insert into ImageTypeModel(ImgType) values('LicenseBack')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
