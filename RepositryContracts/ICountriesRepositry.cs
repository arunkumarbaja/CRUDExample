using System.Reflection;
using Entites;

namespace RepositryContracts
{
	public interface ICountriesRepositry
	{
		/// <summary>
		/// adds a new country to the data store
		/// </summary>
		/// <param name="country"></param>
		/// <returns> return a country object after adding to the data store</returns>
		Task<Country> AddCountry(Country country); //in Service Contracts we use DTOs as arguments for methods where as in in Repository we use Entities as arguments
		                                           
		/// <summary>
		/// 
		/// </summary>
		/// <returns>returns list of countries</returns>
		Task<List<Country>> GetAllCountries();	
		Task<Country?> GetCountryByCountryId(Guid? id);

		Task<Country?> GetCountryByCountryName(string countryName);

	}
}
