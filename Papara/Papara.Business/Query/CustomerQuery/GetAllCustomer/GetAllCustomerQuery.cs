using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Query.CustomerQuery.GetListWithInclude;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerQuery.GetAllCustomer
{

	public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;


	public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
		{
			List<Customer> entityList = await _unitOfWork.CustomerRepository.GetAll("CustomerDetail", "CustomerAddresses", "CustomerPhones");
			var mappedList = _mapper.Map<List<CustomerResponse>>(entityList);
			return new ApiResponse<List<CustomerResponse>>(mappedList);
		}
	}

}
