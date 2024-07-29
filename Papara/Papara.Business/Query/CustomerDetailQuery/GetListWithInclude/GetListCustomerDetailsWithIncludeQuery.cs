using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerDetailQuery.GetListWithInclude
{
	public record GetListCustomerDetailsWithIncludeQuery() : IRequest<ApiResponse<List<CustomerDetailResponseWithInclude>>>;
	public class GetListCustomerDetailWithIncludeQueryHandler : IRequestHandler<GetListCustomerDetailsWithIncludeQuery, ApiResponse<List<CustomerDetailResponseWithInclude>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerDetailWithIncludeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse<List<CustomerDetailResponseWithInclude>>> Handle(GetListCustomerDetailsWithIncludeQuery request, CancellationToken cancellationToken)
		{

			List<CustomerDetail> entityList = await _unitOfWork.CustomerDetailRepository.GetAllInclude(   // Include özelliği null geçilebilir
				include: c => c.Include(c => c.Customer),
				predicate: x => x.IsActive);

			var mappedList = _mapper.Map<List<CustomerDetailResponseWithInclude>>(entityList);
			return new ApiResponse<List<CustomerDetailResponseWithInclude>>(mappedList);
		}
	}
}
