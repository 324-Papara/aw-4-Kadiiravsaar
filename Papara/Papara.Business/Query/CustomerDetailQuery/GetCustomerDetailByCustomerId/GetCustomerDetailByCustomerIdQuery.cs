using AutoMapper;
using MediatR;
using Papara.Base;
using Papara.Base.Response;
using Papara.Business.Query.CustomerDetailQuery.GetById;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerDetailQuery.GetCustomerDetailByCustomerId
{
	public record GetCustomerDetailByCustomerIdQuery : IRequest<ApiResponse<CustomerDetailResponse>>;

	public class GetCustomerDetailByIdQueryHandler :
		 IRequestHandler<GetCustomerDetailByCustomerIdQuery, ApiResponse<CustomerDetailResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ISessionContext _sessionContext;


		public GetCustomerDetailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_sessionContext = sessionContext;
		}
		public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByCustomerIdQuery request, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.CustomerDetailRepository.FirstOrDefault(x => x.CustomerId == _sessionContext.Session.CustomerId, "Customer");
			var mapped = _mapper.Map<CustomerDetailResponse>(entity);
			return new ApiResponse<CustomerDetailResponse>(mapped);
		}
	}
}
