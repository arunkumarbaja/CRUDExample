using Microsoft.EntityFrameworkCore.Migrations;
using System.Net.Http.Headers;

#nullable disable

namespace Entites.Migrations
{
    public partial class DeletePerson_Sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE [dbo].[DeletePerson]
                              (@Name nvarchar(20))
                               AS BEGIN
                               DELETE FROM [dbo].[Persons]  WHERE Name=@Name 
                               END";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = @"DROP PROCEDURE [dbo][DeletePerson]";
            migrationBuilder.Sql(procedure);
		}
    }
}
