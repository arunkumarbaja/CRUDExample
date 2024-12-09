using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// adds an object to the list of countries
        /// </summary>
        /// <param name="CountryAddRequest"></param>
        /// <returns>returns a country object after adding it(including  newly generated country id)</returns>
       Task<CountryResponse> AddCountry(CountryAddRequest?  CountryAddRequest);


        /// <summary>
        /// returns all countries
        /// </summary>
        /// <returns>All Countries from the list of countries as list of CountryResponse</returns>
       Task< List<CountryResponse>> GetAllCountries();

        /// <summary>
        /// Returns CountryResponse Object 
        /// </summary>
        /// <param name="countryID">pass countryID as Argument</param>
        /// <returns>Returns CountryResponse Object </returns>
        Task<CountryResponse>  GetCountryByCountryID(Guid? countryID);

      
    }

}
