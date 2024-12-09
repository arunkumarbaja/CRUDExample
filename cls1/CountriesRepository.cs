using Entites;
using Microsoft.EntityFrameworkCore;
using RepositryContracts;
using System.Security.AccessControl;


namespace Repositaries
{
	public class CountriesRepository : ICountriesRepositry
	{
		private readonly ApplicationDbContext  _db;

		public CountriesRepository(ApplicationDbContext applicationDbContext)
		{
			_db = applicationDbContext;
		}

		public async Task<Country> AddCountry(Country country)
		{
			_db.Countries.Add(country);
	    	await _db.SaveChangesAsync();

			return country;
		}

		public async Task<List<Country>> GetAllCountries()
		{
			return await _db.Countries.ToListAsync();	
		}

		public async Task<Country?> GetCountryByCountryId(Guid? Countryid)
		{
		    return	await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == Countryid);
		}

		public async Task<Country?> GetCountryByCountryName(string countryName)
		{
			return await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryName == countryName);
		}
	}
}
