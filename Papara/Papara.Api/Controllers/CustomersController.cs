using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.Base.Response;
using Papara.Business.Command.CustomerCommand.Create;
using Papara.Business.Command.CustomerCommand.Delete;
using Papara.Business.Command.CustomerCommand.Update;
using Papara.Business.Query.CustomerQuery.GetAllCustomer;
using Papara.Business.Query.CustomerQuery.GetById;
using Papara.Business.Query.CustomerQuery.GetCustomerByCustomerId;
using Papara.Business.Query.CustomerQuery.GetListWithInclude;
using Papara.Business.Query.CustomerQuery.GetParameterQuery;
using Papara.Schema.CustomerSchema;

namespace Papara.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly IMediator mediator;

		public CustomersController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<CustomerResponse>>> Get()
		{
			var operation = new GetAllCustomerQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<CustomerResponse>>> GetByParameters(
			[FromQuery] long? CustomerNumber,
			[FromQuery] string FirstName = null,
			[FromQuery] string LastName = null,
			[FromQuery] string IdentityNumber = null)
		{
			var operation = new GetCustomerByParameterQuery(CustomerNumber, FirstName, LastName, IdentityNumber);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByCustomer")]
		[Authorize(Roles = "customer")]
		public async Task<ApiResponse<CustomerResponse>> GetByCustomerId()
		{
			var operation = new GetCustomerByCustomerIdQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{customerId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<CustomerResponse>> Get([FromRoute] long customerId)
		{
			var operation = new GetCustomerByIdQuery(customerId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
		{
			var operation = new CreateCustomerCommand(value);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{customerId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
		{
			var operation = new UpdateCustomerCommand(customerId, value);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{customerId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> Delete(long customerId)
		{
			var operation = new DeleteCustomerCommand(customerId);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
