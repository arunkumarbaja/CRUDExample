using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace CRUDExample.Controllers
{
	[Route("[controller]")]
	public class AccountController : Controller
	{
		[Route("[action]")]
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[Route("[action]")]
		public IActionResult Register(RegisterDTO registerDTO)
		{
			return RedirectToAction("Index","Persons");
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult Login() 
		{
			return View();	
		}

		

    }


}
