using Entites;
using ServiceContracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        /// <summary>
        /// Represents a DTO class that contains a Person details to update
        /// </summary>
       
             public Guid PersonId { get; set; }

            [Required(ErrorMessage = "Invalid name")]
            public string? Name { get; set; }

            [Required(ErrorMessage = " email address required")]
            [EmailAddress(ErrorMessage = "Invalid email message")]
            public string? Email { get; set; }
            public DateTime? DateOFBirth { get; set; }
            public Guid? CountryID { get; set; }
            public string? Address { get; set; }
            public bool ReceiveNewsLetters { get; set; }

            public Double? Age { get; set; } 

        /// <summary>
        /// Converts current object if PersonAddRequest to object of person type 
        /// </summary>
        /// <returns>Returns Person Objects</returns>
        public Person ToPerson()
        {
            Person p = new Person()
            {
                PersonId = PersonId,
                Name = Name,
                Email = Email,
                DateOFBirth = DateOFBirth,
                CountryID = CountryID,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Address = Address,
                
            };
            return p;

        }
    }
}
