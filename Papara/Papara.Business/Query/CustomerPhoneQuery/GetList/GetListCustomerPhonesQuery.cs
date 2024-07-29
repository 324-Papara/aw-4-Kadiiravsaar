using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerPhoneSchema;


namespace Papara.Business.Query.CustomerPhoneQuery.GetList
{
	public record GetListCustomerPhonesQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
	public class GetListCustomerPhonesQueryHandler : IRequestHandler<GetListCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerPhonesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetListCustomerPhonesQuery request, CancellationToken cancellationToken)
		{
			List<CustomerPhone> entityList = await _unitOfWork.CustomerPhoneRepository.GetAll(); //Include olmayan GetAll ve her veri gelir silinmiiş silinmem,i
			var mappedList = _mapper.Map<List<CustomerPhoneResponse>>(entityList);
			return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
		}
	}

}
