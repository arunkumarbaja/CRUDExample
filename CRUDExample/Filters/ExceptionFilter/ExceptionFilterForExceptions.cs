using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ExceptionFilter
{
	public class ExceptionFilterForExceptions : IExceptionFilter
	{
		private readonly ILogger<ExceptionFilterForExceptions> _logger;

		private readonly IHostEnvironment _hostEnvironment;

		public ExceptionFilterForExceptions(ILogger<ExceptionFilterForExceptions> logger, IHostEnvironment hostEnvironment)
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}
		public void OnException(ExceptionContext context)
		{

			//_logger.LogInformation("Exception Filter {Filtername}.{methodname}\n{ExceptionType}", nameof(ExceptionFilterForExceptions), nameof(OnException),context.Exception.GetType().ToString());

	  //      if(_hostEnvironment.EnvironmentName=="Development" || _hostEnvironment.EnvironmentName == "Staging")
			//context.Result = new ContentResult()
			//{
			//	Content = context.Exception.Message,
			//	StatusCode = 500
			//};
		}
	}
}
