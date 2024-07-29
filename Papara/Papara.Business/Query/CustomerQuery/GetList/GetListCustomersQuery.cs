using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;

namespace Papara.Business.Query.CustomerQuery.GetListWithInclude
{
	public record GetListCustomersQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
	public class GetListCustomersQueryHandler : IRequestHandler<GetListCustomersQuery, ApiResponse<List<CustomerResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetListCustomersQuery request, CancellationToken cancellationToken)
		{
			List<Customer> entityList = await _unitOfWork.CustomerRepository.GetAll("CustomerDetail", "CustomerAddresses", "CustomerPhones");
			var mappedList = _mapper.Map<List<CustomerResponse>>(entityList);
			return new ApiResponse<List<CustomerResponse>>(mappedList);
		}
	}

}