using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    public partial class GetAllPersons_Sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @" CREATE PROCEDURE [dbo].[GetAllPersons]
                                   AS BEGIN
                                   SELECT * FROM [dbo].[Persons]
                                   END";
            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = @" DROP PROCEDURE [dbo].[GetAllPersons]";
			migrationBuilder.Sql(procedure);
		}
    }
}
