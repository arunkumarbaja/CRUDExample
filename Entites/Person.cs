using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;


namespace Entites
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
		[StringLength(20)]
		public string? Name { get; set; }
		[StringLength(100)]
		public string? Email { get; set; }
        public DateTime? DateOFBirth { get; set; }
		[StringLength(6)]
 
		public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
		[StringLength(200)]

		public string? Address { get; set;}
        public bool ReceiveNewsLetters { get; set;}
         public string? TIN { get; set; }
        [ForeignKey("CountryID")]
        public virtual Country? Country_ { get; set; } // this is navigation property that provides Details about all properties of country
                                                       //this nav prop is used instead of joins
    }
}
    