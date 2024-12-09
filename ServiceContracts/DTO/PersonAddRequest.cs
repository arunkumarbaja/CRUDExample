using System;
using ServiceContracts.Enums;
using Entites;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{/// <summary>
/// acts as DTO for inserting a new person
/// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Invalid name")]
        public string? Name { get; set; }

        [Required(ErrorMessage =" email address required")]
        [EmailAddress(ErrorMessage ="Invalid email message")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateOFBirth { get; set; }
		[Required]

		public GenderOptions? Gender { get; set; }
		[Required]

		public Guid? CountryID { get; set; }
		[Required]

		public string? Address { get; set; }
        
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts current object if PersonAddRequest to object of person type 
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            Person p = new Person()
            {
                Name = Name,
                Email = Email,
                DateOFBirth = DateOFBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Address = Address
                
            };
            return p;
        }
    }

  
}
