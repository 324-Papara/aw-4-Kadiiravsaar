using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;

namespace Papara.API.OldController
{

	[Route("api/[controller]")]
	[ApiController]
	[NonController]
	public class Customers2Controller : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public Customers2Controller(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}


		[HttpGet]
		public async Task<List<Customer>> Get()
		{
			var entityList = await unitOfWork.CustomerRepository.GetAll();
			return entityList;
		}

		[HttpGet("{customerId}")]
		public async Task<Customer?> Get(long customerId)
		{
			var entity = await unitOfWork.CustomerRepository.GetById(customerId);
			return entity;
		}

		[HttpPost]
		public async Task Post([FromBody] Customer value)
		{
			await unitOfWork.CustomerRepository.Insert(value);
			await unitOfWork.CustomerRepository.Insert(value);
			await unitOfWork.CustomerRepository.Insert(value);
			await unitOfWork.CompleteWithTransaction();
		}

		[HttpPut("{customerId}")]
		public async Task Put(long customerId, [FromBody] Customer value)
		{
			unitOfWork.CustomerRepository.Update(value);
			await unitOfWork.Complete();
		}

		[HttpDelete("{customerId}")]
		public async Task Delete(long customerId)
		{
			await unitOfWork.CustomerRepository.Delete(customerId);
			await unitOfWork.Complete();
		}
	}
}
