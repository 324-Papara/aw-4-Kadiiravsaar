using AutoMapper;
using MediatR;
using Papara.Base;
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

namespace Papara.Business.Query.CustomerPhoneQuery.GetCustomerPhoneByCustomerId
{
	public record GetCustomerPhoneByCustomerIdQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;

	public class GetCustomerPhoneByCustomerIdQueryHandler : IRequestHandler<GetCustomerPhoneByCustomerIdQuery, ApiResponse<List<CustomerPhoneResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ISessionContext _sessionContext;


		public GetCustomerPhoneByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_sessionContext = sessionContext;
		}


		public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetCustomerPhoneByCustomerIdQuery request, CancellationToken cancellationToken)
		{
			List<CustomerPhone> entityList = await _unitOfWork.CustomerPhoneRepository.Where(x => x.CustomerId == _sessionContext.Session.CustomerId, "Customer");
			var mappedList = _mapper.Map<List<CustomerPhoneResponse>>(entityList);
			return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
		}
	}



}
