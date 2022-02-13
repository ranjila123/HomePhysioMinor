using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePhysio.Migrations
{
    public partial class SeedingForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Geriatric Physiotherapist','It addresses a wide range of issues that affect the elderly people.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Neurological Physiotherapist','It is used to  treat people who have issues with the neurological and neuromuscular systems of the body.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Cardiovascular/Pulmonary Physiotherapist','They treat acute problems like asthma, acute chest infections and trauma; they are involved in the preparation and recovery of patients from major surgery.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Orthopedic Physiotherapist','They specialize in diagnosing and treating conditions that affect any part of your musculoskeletal system.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Womens Health Physiotherapist','They help to treat women’s health issues such as incontinence, pelvic/ vaginal pain, prenatal and postpartum musculoskeletal pain, osteoporosis, rehabilitation following breast surgery, lymphedema, education prevention, wellness and exercise and so much more.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Pre and post surgery Physiotherapist','They provides health and fitness services as well as physical rehabilitation and injury prevention.')");
            migrationBuilder.Sql("insert into CategoryModel(Name,CatInfo) values('Sports Physiotherapist','They are involved in the prevention and management of injuries resulting from sport and exercise participation at all ages and at all levels of ability.')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
