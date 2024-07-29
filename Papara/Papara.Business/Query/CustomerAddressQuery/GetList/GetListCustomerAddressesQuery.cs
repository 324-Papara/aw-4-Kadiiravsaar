using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;


namespace Papara.Business.Query.CustomerAddressQuery.GetList
{
	public record GetListCustomerAddressesQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
	public class GetListCustomerAddressesQueryHandler : IRequestHandler<GetListCustomerAddressesQuery, ApiResponse<List<CustomerAddressResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomerAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetListCustomerAddressesQuery request, CancellationToken cancellationToken)
		{
			//List<CustomerAddress> entityList = await _unitOfWork.CustomerAddressRepository.GetAll(); // Include olmayan GetAll

			List<CustomerAddress> entityList = await _unitOfWork.CustomerAddressRepository.GetAll("Customer");

			var mappedList = _mapper.Map<List<CustomerAddressResponse>>(entityList);
			return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
		}
	}

}
