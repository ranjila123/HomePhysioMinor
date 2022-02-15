using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp) values('d9c5e38d-0cf3-4640-925f-55e6ee6c7daf','Admin','ADMIN','ed886eb7-edda-4f58-8f83-783ea4b76a7a')");
            migrationBuilder.Sql("insert into AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp) values('18c084a6-5d54-4123-aea7-802bb2017975','Patient','PATIENT','a3faf0b4-a307-48c3-825e-20f37f4621ff')");
            migrationBuilder.Sql("insert into AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp) values('4db75558-569a-429c-bc7b-5394fa6acd90','Physiotherapist','PHYSIOTHERAPIST','091ccdd1-e12e-413f-8cae-17d07a267a76')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
