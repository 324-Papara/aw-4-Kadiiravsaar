using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerPhoneSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerPhoneQuery.GetListWithInclude
{

	public record GetListCustomerPhonesWithDetailQuery() : IRequest<ApiResponse<List<CustomerPhoneResponseWithDetail>>>;

	public class GetListCustomerPhonesWithDetailQueryHandler : IRequestHandler<GetListCustomerPhonesWithDetailQuery, ApiResponse<List<CustomerPhoneResponseWithDetail>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerPhonesWithDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse<List<CustomerPhoneResponseWithDetail>>> Handle(GetListCustomerPhonesWithDetailQuery request, CancellationToken cancellationToken)
		{
			List<CustomerPhone> entityList = await _unitOfWork.CustomerPhoneRepository.GetAllInclude(
				include: c => c.Include(c => c.Customer),
				predicate: x => x.IsActive);

			var mappedList = _mapper.Map<List<CustomerPhoneResponseWithDetail>>(entityList);
			return new ApiResponse<List<CustomerPhoneResponseWithDetail>>(mappedList);




		
		}
	}
}
