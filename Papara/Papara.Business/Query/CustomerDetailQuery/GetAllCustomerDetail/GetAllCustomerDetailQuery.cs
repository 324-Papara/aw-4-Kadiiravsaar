using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Query.CustomerDetailQuery.GetList;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerDetailQuery.GetAllCustomerDetail
{
	
    public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;


	public class GetAllCustomerDetailQueryHandler : IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllCustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailQuery request, CancellationToken cancellationToken)
		{
			List<CustomerDetail> entityList = await _unitOfWork.CustomerDetailRepository.GetAll("Customer");
			var mappedList = _mapper.Map<List<CustomerDetailResponse>>(entityList);
			return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
		}
	}
}
