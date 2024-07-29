using AutoMapper;
using MediatR;
using Papara.Base;
using Papara.Base.Response;
using Papara.Business.Query.CustomerQuery.GetById;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerQuery.GetCustomerByCustomerId
{

	public record GetCustomerByCustomerIdQuery : IRequest<ApiResponse<CustomerResponse>>;


	public class GetCustomerByCustomerIdQueryHandler : IRequestHandler<GetCustomerByCustomerIdQuery, ApiResponse<CustomerResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ISessionContext _sessionContext;


		public GetCustomerByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_sessionContext = sessionContext;
		}
		public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByCustomerIdQuery request, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.CustomerRepository.GetById(_sessionContext.Session.CustomerId, "CustomerDetail", "CustomerAddresses", "CustomerPhones");
			var mapped = _mapper.Map<CustomerResponse>(entity);
			return new ApiResponse<CustomerResponse>(mapped);
		}
	}
}
