using CRUDExample.Filters.ActionFilters;
using CRUDExample.Filters.AuthenticationFileter;
using CRUDExample.Filters.ExceptionFilter;
using CRUDExample.Filters.ResultFilters;
using Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System.ComponentModel.Design.Serialization;


namespace CRUDExample.Controllers
{
	[Route("[controller]")]
	[TypeFilter(typeof(PersonsListActionFilters))]
//	[TypeFilter(typeof(ExceptionFilterForExceptions))]

	public class PersonsController : Controller
    {
        private readonly IPersonService _personService;

        private readonly ICountriesService _countriesService;

        private readonly ILogger<PersonsController> _logger;
        
        public PersonsController(IPersonService personService, ICountriesService countriesService ,ILogger<PersonsController> logger)
        {
            _personService = personService;
            _countriesService = countriesService;

            _logger = logger;   
        }

        [Route("[action]")]
        [Route("/")]
        [TypeFilter(typeof(PersonsListActionFilters))] //By default  order value is zero
        [TypeFilter(typeof(PersonsListResultFilter))]
      //  [TypeFilter(typeof(ExceptionFilterForExceptions))]
        public async Task<IActionResult> Index(String? SearchBy, string? SearchString, string SortBy = nameof(PersonResponse.Name), SortOrderOptions SortOrder = SortOrderOptions.DSC)
        {
            //Adding Log Information


            _logger.LogInformation("Index action Method");

			//displaying current values of variables through LogDebugg

			_logger.LogDebug($"SearchBy : {SearchBy} \n SortBy{SortBy}  \n SearchString: {SearchString}");


			//Searching Functionality
			ViewBag.Search = new Dictionary<string, string>()
            {
                { "Select", "Select"},
                {nameof(PersonResponse.Name), "Person Name"},
                {nameof(PersonResponse.Email), "Email"},
                {nameof(PersonResponse.DateOFBirth), "Date Of Birth"},
                {nameof(PersonResponse.Gender), "Gender"},
                {nameof(PersonResponse.Address), "Address"},
                {nameof(PersonResponse.ReceiveNewsLetters), "Receive New Letter"},
            };

           List<PersonResponse> persons =await _personService.GetPersonsFilters(SearchBy,SearchString);

            ViewBag.CurrentSearchBy = SearchBy;
            ViewBag.CurrrentSearchString = SearchString;



            // Sorting Functionality

           List<PersonResponse> sortedPersons = await _personService.GetSortedPersons(persons, SortBy,SortOrder);

            ViewBag.CurrentSortBy = SortBy;    
            ViewBag.CurrentSortOrder = SortOrder.ToString();

            return View(sortedPersons);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
           List<CountryResponse> country_list =await _countriesService.GetAllCountries();

            ViewBag.CountryList = country_list.Select(temp=>new SelectListItem()
            {
                Text=temp.CountryName,Value=temp.CountryID.ToString()
            });

            //new SelectListItem() { Text="arun",Value="1"};
            //<options value="1">arun</options>
            return View();  
        }
        [HttpPost]
        [Route("[action]")]
		public async Task<IActionResult> Create(PersonAddRequest? personAddRequest)
		{
            if(ModelState.IsValid==false)
            {
				List<CountryResponse> country_list =await _countriesService.GetAllCountries();
				ViewBag.CountryList = country_list;

                ViewBag.error = ModelState.Values.SelectMany(v=>v.Errors).Select(e=>e.ErrorMessage).ToList();
                return View();
			}

           await _personService.AddPerson(personAddRequest);

			return RedirectToAction("Index");
		}
        [HttpGet]
        [Route("[action]/{personID}")]
        [TypeFilter(typeof(TokenResultfilterCookies))]
        public async Task<IActionResult>Edit(Guid personID)
        {
            List<CountryResponse> country_list = await _countriesService.GetAllCountries();

            ViewBag.CountryList = country_list.Select(temp => new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryID.ToString()
            });


            PersonResponse? personresponse =await _personService.GetPersonByPersonId(personID);
            if (personresponse == null)
            {
                return RedirectToAction("Index", "persons");
            }
            PersonUpdateRequest personUpdateRequest = personresponse.ToPersonUpdateRequest();
           
            return View(personUpdateRequest);  
        }

		[HttpPost]
        [Route("[action]/{personID}")]
		[TypeFilter(typeof(AuthenticationFilterForEdit))]

		public async Task< IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personresponse =await _personService.GetPersonByPersonId(personUpdateRequest.PersonId);
            if (personresponse == null)
            {
                return RedirectToAction("Index","persons");
            }
            if (ModelState.IsValid==true)
            {
              await  _personService.updatePerson(personUpdateRequest); 
                 return RedirectToAction("Index","Persons");        
            }
            if (ModelState.IsValid == false)
            {
				List<CountryResponse> country_list =await _countriesService.GetAllCountries();
				ViewBag.CountryList = country_list.Select(temp => new SelectListItem()
				{
					Text = temp.CountryName,
					Value = temp.CountryID.ToString()
				});

				ViewBag.error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            return View();
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public async Task<IActionResult> Delete(Guid personId)
        {
         PersonResponse? personResponse=await _personService.GetPersonByPersonId(personId);
            if (personResponse == null)
            {
                return RedirectToAction("Index","Persons");
            }
            return View(personResponse);  
        }
		[HttpPost]
		[Route("[action]/{personId}")]
		public async Task<IActionResult> Delete(PersonUpdateRequest? personUpdateRequest)
		{
			PersonResponse? personResponse =await _personService.GetPersonByPersonId(personUpdateRequest?.PersonId);

			if (personResponse == null)
			{
				return RedirectToAction("Index", "Persons");
			}
           await _personService.DeletePerson(personResponse?.PersonId);

            return RedirectToAction("Index");
		}

        [Route("/PersonsPdf")]
        public async Task<IActionResult> PersonsPdf()
        {
            //Get list of all persons
           List<PersonResponse> personslist=await _personService.GetAllPersons();

            //Generate Pdf
            ViewAsPdf viewAsPdf = new ViewAsPdf("PersonsPdf", personslist, ViewData)
            {
                PageMargins=new Rotativa.AspNetCore.Options.Margins()
                {
                    Top=20, Bottom=20, Right=20,Left=20,
                    
                },
			   PageOrientation=	Rotativa.AspNetCore.Options.Orientation.Portrait

			};

            return viewAsPdf;
        }

        [Route("[action]")]
        public async Task<IActionResult> PersonsCSV() 
        {
          MemoryStream memoryStream = await _personService.GetPersonsCSV();

            return File(memoryStream, "application/octet-stream", "person.csv");
        }

        [Route("[action]")]
        public async Task<IActionResult> PersonsExcel()
        {
            MemoryStream memoryStream = await _personService.GetPersonsExcel();

            return File(memoryStream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","persons.xlsx");
        }
    }
}
