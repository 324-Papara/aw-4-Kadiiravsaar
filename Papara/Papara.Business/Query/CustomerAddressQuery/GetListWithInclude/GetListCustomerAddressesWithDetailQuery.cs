using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerAddressQuery.GetListWithInclude
{
	
	public record GetListCustomerAddressesWithDetailQuery() : IRequest<ApiResponse<List<CustomerAddressResponseWithDetail>>>;
	public class GetListCustomerAddressesWithDetailQueryHandler : IRequestHandler<GetListCustomerAddressesWithDetailQuery, ApiResponse<List<CustomerAddressResponseWithDetail>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerAddressesWithDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerAddressResponseWithDetail>>> Handle(GetListCustomerAddressesWithDetailQuery request, CancellationToken cancellationToken)
		{
			
			List<CustomerAddress> entityList = await _unitOfWork.CustomerAddressRepository.GetAllInclude(   // Include özelliği null geçilebilir
				include: c => c.Include(c => c.Customer),
				predicate: x => x.IsActive);

			var mappedList = _mapper.Map<List<CustomerAddressResponseWithDetail>>(entityList);
			return new ApiResponse<List<CustomerAddressResponseWithDetail>>(mappedList);
		}
	}
}
