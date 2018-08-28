using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Migrations
{
    public partial class MigrateEmptyDateOfBirthToNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE [dbo].[Users] SET [DateOfBirth] = NULL WHERE [DateOfBirth] = '1/1/0001 12:00:00 AM'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
