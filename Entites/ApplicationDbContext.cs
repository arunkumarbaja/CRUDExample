using CRUDExample.Identity_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entites
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
	{
	
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public virtual DbSet<Person> Persons { get; set;}
		public virtual DbSet<Country> Countries { get; set;}



		protected override void OnModelCreating(ModelBuilder modelBuilder) // we can configure the tables, indexes, primary keys ,seed data using modelBuilder object
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>().ToTable("Countries"); //Maping/binding countries DbSet to the Countries Table
			modelBuilder.Entity<Person>().ToTable("Persons");    //Maping/binding Persons DbSet to the Persons Table

			//Seed to Countries (adding initial data when applications is loaded)
			String CountriesLIst=System.IO.File.ReadAllText("countries.json");

		    List<Country>? countries=	System.Text.Json.JsonSerializer.Deserialize<List<Country>>(CountriesLIst);
			
			foreach(Country country in countries)
				modelBuilder.Entity<Country>().HasData(country);
			
			//Seed to Persons

		    String personslist=	System.IO.File.ReadAllText("persons.json");
			List<Person>? persons= System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personslist);

			foreach (Person person in persons)
				modelBuilder.Entity<Person>().HasData(person);


			            //Fluent Api
			modelBuilder.Entity<Person>().Property(temp => temp.TIN)
				.HasColumnName("TaxIdentificationNumber")
				.HasColumnType("varchar(10)")
				.HasDefaultValue("ABC123");

			      //Create an Index for TIN for that helps in fast Searches
		 //modelBuilder.Entity<Person>().HasIndex(temp=>temp.TIN).IsUnique();

               //Adding Contraint on TIN
			modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 6");
		}
		public List<Person> GetAllPersons_Sp()
		{
			return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
		}

		public int InsertPerson(Person person)
		{
			SqlParameter[] sp = new SqlParameter[]
			{ 
				new SqlParameter("@PersonId",person.PersonId),
				new SqlParameter("@Name",person.Name),
				new SqlParameter("@Email",person.Email),
				new SqlParameter("@DateOFBirth",person.DateOFBirth),
				new SqlParameter("@Gender",person.Gender),
				new SqlParameter("@CountryID",person.CountryID),
				new SqlParameter("@Address",person.Address),
				new SqlParameter("@ReceiveNewsLetters",person.ReceiveNewsLetters),
			};

		    return	Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonId,@Name,@Email,@DateOFBirth,@Gender,@CountryID,@Address,@ReceiveNewsLetters",sp);
		}
		public int DeletePerson(Person person)
		{
			SqlParameter[] sqlParameters = new SqlParameter[]
			{
				new SqlParameter("@Name",person.Name)
			};

		  return 	Database.ExecuteSqlRaw("EXECUTE [dbo].[DeletePerson] @Name", sqlParameters);
		}

		public int UpdatePersonDetails(Person person)
		{
			SqlParameter[] sp = new SqlParameter[]
			{
				new SqlParameter("@PersonId",person.PersonId),
				new SqlParameter("@Name",person.Name),
				new SqlParameter("@Email",person.Email),
				new SqlParameter("@DateOFBirth",person.DateOFBirth),
				new SqlParameter("@Gender",person.Gender),
				new SqlParameter("@CountryID",person.CountryID),
				new SqlParameter("@Address",person.Address),
				new SqlParameter("@ReceiveNewsLetters",person.ReceiveNewsLetters),
			};

			return Database.ExecuteSqlRaw("EXECUTE [dbo].[UpdatePerson] @PersonId,@Name,@Email,@DateOFBirth,@Gender,@CountryID,@Address,@ReceiveNewsLetters", sp);

		}
	}
	


}
