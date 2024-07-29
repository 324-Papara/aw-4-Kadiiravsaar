using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;


namespace Papara.Business.Query.CustomerDetailQuery.GetById
{
	public record GetCustomerDetailByIdQuery(long CustomerDetailId) : IRequest<ApiResponse<CustomerDetailResponseWithInclude>>;

	public class GetCustomerDetailByIdQueryHandler : IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponseWithInclude>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCustomerDetailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ApiResponse<CustomerDetailResponseWithInclude>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
		{
			//var entity = await _unitOfWork.CustomerRepository.GetById(request.CustomerId);
			
			var entity = await _unitOfWork.CustomerDetailRepository.GetInclude(
				request.CustomerDetailId,
				include: c => c.Include(c => c.Customer));


			if (entity == null)
			{
				return new ApiResponse<CustomerDetailResponseWithInclude>(false)
				{
					IsSuccess = false,
					Message = "Customer detail not found"
				};
			}

			var mapped = _mapper.Map<CustomerDetailResponseWithInclude>(entity);
			return new ApiResponse<CustomerDetailResponseWithInclude>(mapped);
		}
	}
}
