using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.Base.Response;
using Papara.Business.Command.CustomerAddressCommand.Create;
using Papara.Business.Command.CustomerAddressCommand.Delete;
using Papara.Business.Command.CustomerAddressCommand.Update;
using Papara.Business.Query.CustomerAddressQuery.GetById;
using Papara.Business.Query.CustomerAddressQuery.GetCustomerAddressByCustomerId;
using Papara.Business.Query.CustomerAddressQuery.GetList;
using Papara.Business.Query.CustomerAddressQuery.GetListWithInclude;
using Papara.Schema.CustomerAddressSchema;

namespace Papara.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerAddressesController : ControllerBase
	{
		private readonly IMediator mediator;

		public CustomerAddressesController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
		{
			var operation = new GetListCustomerAddressesQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{CustomerAddressId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<CustomerAddressResponse>> GetById([FromRoute] long CustomerAddressId)
		{
			var operation = new GetCustomerAddressByIdQuery(CustomerAddressId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByCustomer")]
		[Authorize(Roles = "customer")]
		public async Task<ApiResponse<List<CustomerAddressResponse>>> GetByCustomerId()
		{
			var operation = new GetCustomerAddressByCustomerIdQuery();
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
		{
			var operation = new CreateCustomerAddressCommand(value);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{CustomerAddressId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> Put(long CustomerAddressId, [FromBody] CustomerAddressRequest value)
		{
			var operation = new UpdateCustomerAddressCommand(CustomerAddressId, value);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{CustomerAddressId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> Delete(long CustomerAddressId)
		{
			var operation = new DeleteCustomerAddressCommand(CustomerAddressId);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
