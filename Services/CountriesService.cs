using Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RepositryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Collections;
using System.Data;

namespace Services
{
    
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepositry  _countriesRepository;  

        public CountriesService(ICountriesRepositry countryRepository)
        {
			_countriesRepository = countryRepository; 
        }
        public async Task<CountryResponse> AddCountry(CountryAddRequest? CountryAddRequest)
        {
            // validations:CountryAddRequest Cannot nbe null
            if(CountryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(CountryAddRequest));
            }
            //validation:if country name is null

           if(CountryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(CountryAddRequest.CountryName)); 
            }
           //validation : Country name should cannot be duplicate

           if(_countriesRepository.GetCountryByCountryName(CountryAddRequest.CountryName) !=null) 
            {
                throw new DuplicateNameException(nameof(CountryAddRequest.CountryName)); 
            }
           

            //convert object from CountryAddRequest to country Type
            Country country=  CountryAddRequest.ToCountry();

            //genarate new CountryId

            country.CountryID =  Guid.NewGuid();

            //add country object to _countries

           await _countriesRepository.AddCountry(country);

            return country.ToCountryResponse();
        }

        public async Task< List<CountryResponse>> GetAllCountries()
        {
             List<Country> countries= await _countriesRepository.GetAllCountries();

			//converts country object to CountryResponse object
			List<CountryResponse> countryResponses=   countries.Select(country=> country.ToCountryResponse()).ToList();

            return countryResponses;
        }

        public async Task<CountryResponse> GetCountryByCountryID(Guid? countryID)
        {
            //Checking if country id is not null
            if(countryID == null)
                return null;

            //get matching country from List<Country> based CountryID
            Country? country_from_list = await _countriesRepository.GetCountryByCountryId(countryID);

            if (country_from_list == null)
                return null;


            //Converting Country object mto CountryResponse object
            CountryResponse? countryResponse_from_obj= country_from_list.ToCountryResponse();

            //Returning CountryResponse Object
            return countryResponse_from_obj;    
        }
    }
}
