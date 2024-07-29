using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.Business.Query.CustomerReport;

namespace Papara.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CustomerReportController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CustomerReportController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomerReport()
		{
			var operation = await _mediator.Send(new GetCustomerReportQuery());
			return Ok(operation);
		}
	}

}
