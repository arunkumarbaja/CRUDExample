using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	public class RegisterDTO
	{

		[Required (ErrorMessage ="Person Name cannot be blank")]
		[DisplayName("Person Name")]
		public string? PersonName { get; set; }
		[Required(ErrorMessage = "Email cannot be blank")]
		[EmailAddress]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Phone number cannot be blank")]
		[DataType(DataType.PhoneNumber)]
		[DisplayName("Phone Number")]
		public string? PhoneNumber { get; set; }
		[Required(ErrorMessage = "Password cannot be blank")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required(ErrorMessage = "Confirm Password cannot be blank")]
		[DataType(DataType.Password)]
		[DisplayName("Confirm Password")]
	    [Compare("Password",ErrorMessage ="password and confirm password do not match")]    
		public string? ConfirmPassword { get; set; }	
	
	}
}
