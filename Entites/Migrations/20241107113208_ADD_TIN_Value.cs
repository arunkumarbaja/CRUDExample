using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    public partial class ADD_TIN_Value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string Query = @"UPDATE [Persons] SET TaxIdentificationNumber='ABC123' ";
            migrationBuilder.Sql(Query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string Query = @"UPDATE [Persons] SET TaxIdentificationNumber='null' ";
            migrationBuilder.Sql(Query);

        }
    }
}
