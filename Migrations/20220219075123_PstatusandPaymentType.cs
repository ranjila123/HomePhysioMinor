using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class PstatusandPaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into PStatusModel(PStatuCode,PStatusType) values('1','Unpaid')");
            migrationBuilder.Sql("insert into PStatusModel(PStatuCode,PStatusType) values('2','Advance')");
            migrationBuilder.Sql("insert into PStatusModel(PStatuCode,PStatusType) values('3','Paid')");

            migrationBuilder.Sql("insert into PaymentTypeModel(PaymentTypeName) values('Khalti')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
