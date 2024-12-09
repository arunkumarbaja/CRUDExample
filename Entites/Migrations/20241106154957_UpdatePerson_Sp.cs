using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    public partial class UpdatePerson_Sp : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string procedure = @"CREATE PROCEDURE [dbo].[UpdatePerson]
                                (@PersonId uniqueidentifier,@Name nvarchar(20),
                                @Email nvarchar(100),@DateOFBirth datetime2(7),
                                @Gender nvarchar(6), @CountryID uniqueidentifier,
                                @Address nvarchar(200),@ReceiveNewsLetters bit )
                                AS BEGIN
                                UPDATE [dbo].[Persons] SET Name=@Name,
                                Email=@Email,DateOFBirth=@DateOFBirth,Gender=@Gender,
                                Address=@Address,CountryID=@CountryID,ReceiveNewsLetters=@ReceiveNewsLetters  WHERE PersonId=@PersonId
                                END";
			migrationBuilder.Sql(procedure);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string procedure = @"CREATE PROCEDURE [dbo].[UpdatePerson]";
			migrationBuilder.Sql(procedure);
		}
	}
}
