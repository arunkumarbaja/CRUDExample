using CRUDExample.Filters.ActionFilters;
using CRUDExample.Identity_Entities;
using Entites;
using Microsoft.EntityFrameworkCore;
using Repositaries;
using RepositryContracts;
using Serilog;
using ServiceContracts;
using Services;

namespace CRUDExample.StartupExtensions
{
	public static class ConfigureServiceExtensions
	{
		public  static IServiceCollection ConfigureServices(this IServiceCollection services , IConfiguration configuration)
		{


			services.AddControllersWithViews(options =>
			{
				// Adding  action filter  Globally that does not have any arguments
				options.Filters.Add<PersonsListActionFilters>();

				//way for accessing Logger Service in the class
				var logger = services.BuildServiceProvider().GetRequiredService<ILogger<PersonsListActionFilters>>();

				//another way for adding filter globally

				options.Filters.Add(new PersonsListActionFilters(logger));
			});

			services.AddScoped<ICountriesService, CountriesService>();
			services.AddScoped<IPersonService, PersonsService>();
			services.AddScoped<ICountriesRepositry, CountriesRepository>();
			services.AddScoped<IPersonsRepositry, PersonsRepository>();
			services.AddDbContext<ApplicationDbContext>(
				options =>
				{
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); // we are conforming that we are using SqlServer for database connection

				});
			



			// Registering HttpLogging as a Service 
			services.AddHttpLogging(options =>
			{
				options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
			});


			return services;	

		}
	}
}
