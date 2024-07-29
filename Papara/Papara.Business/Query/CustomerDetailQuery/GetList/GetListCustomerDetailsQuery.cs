using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;


namespace Papara.Business.Query.CustomerDetailQuery.GetList
{
	public record GetListCustomerDetailsQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
	public class GetListCustomerDetailQueryHandler : IRequestHandler<GetListCustomerDetailsQuery, ApiResponse<List<CustomerDetailResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetListCustomerDetailsQuery request, CancellationToken cancellationToken)
		{
			List<CustomerDetail> entityList = await _unitOfWork.CustomerDetailRepository.GetAll();  //Include olmayan GetAll
			var mappedList = _mapper.Map<List<CustomerDetailResponse>>(entityList);
			return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
		}
	}
}
