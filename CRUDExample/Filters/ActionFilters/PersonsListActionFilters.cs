using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using System.Security.Cryptography.Pkcs;
namespace CRUDExample.Filters.ActionFilters
{
	public class PersonsListActionFilters : IActionFilter/*, IOrderedFilter*/  m 
	{
		private readonly ILogger<PersonsListActionFilters> _logger; 

		public int Order { get; set; }
		public PersonsListActionFilters(ILogger<PersonsListActionFilters> logger/*, int order*/ )
		{
			_logger = logger;
			//int Order = order;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation("PersonsList OnActionExecuted Action Filter");
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{

			// To do : if SearchBy Parameter provided by user is null it automatically resets to Name(parameter)


		    String SearchBy=	context.ActionArguments.ContainsKey("SearchBy").ToString();

			//Validating SearchBy parameter value
			if( ! String.IsNullOrEmpty(SearchBy))
			{
				var SearchOptions = new List<String>()
				{
					nameof(PersonResponse.Name),
					nameof(PersonResponse.Email),
					nameof(PersonResponse.Gender),
					nameof(PersonResponse.DateOFBirth),
					nameof(PersonResponse.Country),
					nameof(PersonResponse.ReceiveNewsLetters),
				};
				//resetting the SearchBy Parameter value
				if (SearchOptions.Any(temp => temp == SearchBy) == false)
				{
					//Structured Logging=> passing parameters in Log information is called Structured logging
					_logger.LogInformation($"SearchBY Actual value {SearchBy}");

					//context.ActionArguments["SearchBy"] = nameof(PersonResponse.Name);	
				}
			}

			_logger.LogInformation("PersonsList OnActionExecuting Action Filter");
	    }
	}
}
