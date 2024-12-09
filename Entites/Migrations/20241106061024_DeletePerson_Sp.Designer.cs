﻿// <auto-generated />
using System;
using Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entites.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241106061024_DeletePerson_Sp")]
    partial class DeletePerson_Sp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entites.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            CountryName = "Philippines"
                        },
                        new
                        {
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            CountryName = "Thailand"
                        },
                        new
                        {
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            CountryName = "China"
                        },
                        new
                        {
                            CountryID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            CountryName = "Palestinian Territory"
                        },
                        new
                        {
                            CountryID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            CountryName = "China"
                        });
                });

            modelBuilder.Entity("Entites.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOFBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("ReceiveNewsLetters")
                        .HasColumnType("bit");

                    b.HasKey("PersonId");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("5523b350-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "4 Parkside Point",
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DateOFBirth = new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mwebsdale0@people.com.cn",
                            Gender = "Female",
                            Name = "Marguerite",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("5523b351-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "6 Morningstar Circle",
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DateOFBirth = new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ushears1@globo.com",
                            Gender = "Female",
                            Name = "Ursa",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("5523b352-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "73 Heath Avenue",
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DateOFBirth = new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "fbowsher2@howstuffworks.com",
                            Gender = "Male",
                            Name = "Franchot",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5523b353-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "83187 Merry Drive",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOFBirth = new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "asarvar3@dropbox.com",
                            Gender = "Male",
                            Name = "Angie",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5523b354-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "50467 Holy Cross Crossing",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOFBirth = new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ttregona4@stumbleupon.com",
                            Gender = "Gender",
                            Name = "Tani",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("5523b355-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "97570 Raven Circle",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOFBirth = new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mlingfoot5@netvibes.com",
                            Gender = "Male",
                            Name = "Mitchael",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("5523b356-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "57449 Brown Way",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOFBirth = new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mjarrell6@wisc.edu",
                            Gender = "Male",
                            Name = "Maddy",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5523b357-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "4 Stuart Drive",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOFBirth = new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pretchford7@virginia.edu",
                            Gender = "Female",
                            Name = "Pegeen",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5523b358-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "413 Sachtjen Way",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOFBirth = new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "hmosco8@tripod.com",
                            Gender = "Male",
                            Name = "Hansiain",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5523b359-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "484 Clarendon Court",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOFBirth = new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lwoodwing9@wix.com",
                            Gender = "Male",
                            Name = "Lombard",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("5523b360-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "2 Warrior Avenue",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOFBirth = new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mconachya@va.gov",
                            Gender = "Female",
                            Name = "Minta",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("5513b360-a5dc-48fe-ac74-cabbc02c7fda"),
                            Address = "9334 Fremont Street",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOFBirth = new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vklussb@nationalgeographic.com",
                            Gender = "Female",
                            Name = "Verene",
                            ReceiveNewsLetters = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
