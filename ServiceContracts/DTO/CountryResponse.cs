using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entites;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class id used as  return type for most of CountriesService Methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }

        public string? CountryName { get; set; }


        //it compares the current object to another object of country response type and returns true , if not returns false
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;  
            if(obj.GetType()!= typeof(CountryResponse))
            {
                return false;   
            }

             CountryResponse country_obj= (CountryResponse)obj;

             return this.CountryID == country_obj.CountryID && this.CountryName == country_obj.CountryName;

        }

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)//(this Country country) represents  binding of ToCountryResponse method Country class
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,

                CountryName = country.CountryName,
            };
        }
    }
        
}
