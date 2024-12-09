using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;
using System.Threading.Tasks;

namespace CRUDExample.ExceptionMiddleware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		private readonly IDiagnosticContext _diagnosticContext;
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IDiagnosticContext diagnosticContext)
		{
			_next = next;
			_diagnosticContext = diagnosticContext;
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
			   await _next(httpContext); // next delegate represents execution of next middleware
			}
			catch(Exception e)
			{
				if(e.InnerException!=null)
				{
					_logger.LogError("{ExceptionType} {ExceptionMessage}", e.GetType().ToString(), e.Message.ToString());
				}
				else
				{
					_logger.LogError("{ExceptionType} {ExceptionMessage}", e.GetType().ToString(), e.Message.ToString());
				}
				//httpContext.Response.StatusCode = 500;

				throw; // we are rethorwing exception to the Exception handler Middleware
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class ExceptionHandlingMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
