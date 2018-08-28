using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Migrations.Identity
{
    public partial class ScriptDefaultRoles : Migration
    {
        public static readonly string NormalizedAdminName = "ADMIN";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                IF NOT EXISTS(SELECT 1 FROM [Identity].[AspNetRoles] WHERE NormalizedName = '{NormalizedAdminName}')
                    INSERT INTO [Identity].[AspNetRoles] (Id, Name, NormalizedName) VALUES (NEWID(), 'Admin', '{NormalizedAdminName}')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DELETE FROM [Identity].[AspNetRoles] WHERE NormalizedName = '{NormalizedAdminName}'");
        }
    }
}
