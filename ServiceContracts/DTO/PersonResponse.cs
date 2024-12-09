using Entites;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;


namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOFBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// Compares current object with the parameter object
        /// </summary>
        /// <param name="obj">PersonResponse object to compare</param>
        /// <returns> True or False indicating weather all details are matched with the specified parameter object </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(PersonResponse))
                return base.Equals(obj);

            PersonResponse person_compare = (PersonResponse)obj;

            return this.Name == person_compare.Name
                   && this.Email == person_compare.Email
                   && this.DateOFBirth == DateOFBirth
                   && this.Gender == Gender
                   && this.DateOFBirth == DateOFBirth
                   && this.Country == Country
                   && this.CountryID == CountryID
                   && this.Address == Address
                   && this.Age == Age;

        }


        public override string ToString()
        {
            return $"PersonID :{PersonId} ,Name {Name} , Gender {GenderOptions.male} , Email {Email} ," +
                   $" Dae of Birth {DateOFBirth?.ToString()} ,Country ID {CountryID}, Country Name{Country}, " +
                   $"address{Address},ReceiveNewsLetters{ReceiveNewsLetters}";
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            PersonUpdateRequest p = new PersonUpdateRequest()
            {
                PersonId = PersonId,
                Name = Name,
                Email = Email,
                DateOFBirth = DateOFBirth,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Age = Age,
                
            } ;
            return p;
        }

    }

    public static class PersonExtensionClass
     {
        /// <summary>
        /// extension method converts person object of Person Class to PersonResponse Object 
        /// </summary>
         public static PersonResponse ToPersonResponse(this Person p)
         {
            PersonResponse personResponse = new PersonResponse()
            {
                PersonId = p.PersonId,
                Name = p.Name,
                Email = p.Email,
                DateOFBirth = p.DateOFBirth,
                Gender = p.Gender,
                CountryID = p.CountryID,
                Address = p.Address,
                ReceiveNewsLetters = p.ReceiveNewsLetters,
                Age = (p.DateOFBirth != null) ? Math.Round((DateTime.Now - (p.DateOFBirth).Value).TotalDays / 365.25) : null,
                Country = p.Country_?.CountryName
               
               
            };
            return personResponse;
         }
    }
}
