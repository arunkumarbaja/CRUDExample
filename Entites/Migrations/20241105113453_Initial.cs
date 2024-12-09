using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOFBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "China" },
                    { new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "Philippines" },
                    { new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "China" },
                    { new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), "Thailand" },
                    { new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "Palestinian Territory" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryID", "DateOFBirth", "Email", "Gender", "Name", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("5513b360-a5dc-48fe-ac74-cabbc02c7fda"), "9334 Fremont Street", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "vklussb@nationalgeographic.com", "Female", "Verene", true },
                    { new Guid("5523b350-a5dc-48fe-ac74-cabbc02c7fda"), "4 Parkside Point", new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "mwebsdale0@people.com.cn", "Female", "Marguerite", false },
                    { new Guid("5523b351-a5dc-48fe-ac74-cabbc02c7fda"), "6 Morningstar Circle", new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ushears1@globo.com", "Female", "Ursa", false },
                    { new Guid("5523b352-a5dc-48fe-ac74-cabbc02c7fda"), "73 Heath Avenue", new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbowsher2@howstuffworks.com", "Male", "Franchot", true },
                    { new Guid("5523b353-a5dc-48fe-ac74-cabbc02c7fda"), "83187 Merry Drive", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "asarvar3@dropbox.com", "Male", "Angie", true },
                    { new Guid("5523b354-a5dc-48fe-ac74-cabbc02c7fda"), "50467 Holy Cross Crossing", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "ttregona4@stumbleupon.com", "Gender", "Tani", false },
                    { new Guid("5523b355-a5dc-48fe-ac74-cabbc02c7fda"), "97570 Raven Circle", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "mlingfoot5@netvibes.com", "Male", "Mitchael", false },
                    { new Guid("5523b356-a5dc-48fe-ac74-cabbc02c7fda"), "57449 Brown Way", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "mjarrell6@wisc.edu", "Male", "Maddy", true },
                    { new Guid("5523b357-a5dc-48fe-ac74-cabbc02c7fda"), "4 Stuart Drive", new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "pretchford7@virginia.edu", "Female", "Pegeen", true },
                    { new Guid("5523b358-a5dc-48fe-ac74-cabbc02c7fda"), "413 Sachtjen Way", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "hmosco8@tripod.com", "Male", "Hansiain", true },
                    { new Guid("5523b359-a5dc-48fe-ac74-cabbc02c7fda"), "484 Clarendon Court", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lwoodwing9@wix.com", "Male", "Lombard", false },
                    { new Guid("5523b360-a5dc-48fe-ac74-cabbc02c7fda"), "2 Warrior Avenue", new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "mconachya@va.gov", "Female", "Minta", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
