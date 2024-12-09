using Microsoft.AspNetCore.Identity;
namespace CRUDExample.Identity_Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string? UserName { get; set; }	
	}
}
