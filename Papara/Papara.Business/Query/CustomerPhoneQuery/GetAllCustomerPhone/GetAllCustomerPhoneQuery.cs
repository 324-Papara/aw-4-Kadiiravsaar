using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Query.CustomerPhoneQuery.GetList;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerPhoneSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerPhoneQuery.GetAllCustomerPhone
{
	public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;

	public class GetAllCustomerPhoneQueryHandler : IRequestHandler<GetAllCustomerPhoneQuery, ApiResponse<List<CustomerPhoneResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllCustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken)
		{
			List<CustomerPhone> entityList = await _unitOfWork.CustomerPhoneRepository.GetAll("Customer");
			var mappedList = _mapper.Map<List<CustomerPhoneResponse>>(entityList);
			return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
		}
	}
}
