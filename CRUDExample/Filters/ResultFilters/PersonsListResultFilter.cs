using Microsoft.AspNetCore.Mvc.Filters;
using Xunit.Sdk;

namespace CRUDExample.Filters.ResultFilters
{
	public class PersonsListResultFilter : IAsyncResultFilter
	{

		private readonly ILogger<PersonsListResultFilter> _logger;
		public PersonsListResultFilter(ILogger<PersonsListResultFilter> logger)
		{
			_logger = logger;
		}
		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			////To Do Before Logic
			//_logger.LogInformation($"{nameof(PersonsListResultFilter)} nameof{OnResultExecutionAsync}");

			await next();
			////To do After Logic

			//  _logger.LogInformation($"{nameof(PersonsListResultFilter)} nameof{OnResultExecutionAsync}");

			//context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString("yyyy-mm-dd HH:mm");

			throw new NotImplementedException();

		}
	}
}
