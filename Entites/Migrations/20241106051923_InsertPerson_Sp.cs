using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    public partial class InsertPerson_Sp : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string procedure = @"CREATE PROCEDURE [dbo].[InsertPerson]
                               (@PersonId uniqueidentifier,@Name nvarchar(20),
                                @Email nvarchar(100),@DateOFBirth datetime2(7),
                                @Gender nvarchar(6), @CountryID uniqueidentifier,
                                @Address nvarchar(200),@ReceiveNewsLetters bit )
                                AS BEGIN
                                INSERT INTO [dbo].[Persons] (PersonId,Name,Email,DateOFBirth,
                                Gender,CountryID,Address,ReceiveNewsLetters) values(@PersonId ,@Name,
                                @Email ,@DateOFBirth , @Gender , @CountryID , @Address ,@ReceiveNewsLetters )
                                END";

			migrationBuilder.Sql(procedure);

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string procedure = @"DROP PROCEDURE [dbo].[InsertPerson]";

			migrationBuilder.Sql(procedure);
		}
	}
}
