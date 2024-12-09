using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace CRUDExample.Filters.AuthenticationFileter
{
	public class AuthenticationFilterForEdit : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if(context.HttpContext.Request.Cookies.ContainsKey("Auth-Key")==false)
			{
				//if any not null vaule is assigned to context.result then filter pipeline will be short circuited
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized); 

			}
			if (context.HttpContext.Request.Cookies["Auth-Key"] != "A100")
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);

			}
		}
	}
}


