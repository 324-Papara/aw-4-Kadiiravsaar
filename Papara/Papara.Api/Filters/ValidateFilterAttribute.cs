using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Papara.Base.Response;
using Papara.Schema;
using Serilog;

namespace Papara.API.Filters
{
	public class ValidateFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
				context.Result = new BadRequestObjectResult(ApiResponse<NoContentResponse>.Fail(errors));

			}
		}
	}

}
