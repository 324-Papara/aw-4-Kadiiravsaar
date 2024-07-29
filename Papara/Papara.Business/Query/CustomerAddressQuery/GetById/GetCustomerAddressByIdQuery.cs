using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerAddressQuery.GetById
{

	public record GetCustomerAddressByIdQuery(long CustomerAddressId) : IRequest<ApiResponse<CustomerAddressResponse>>;

	public class GetCustomerAddressByIdQueryHandler : IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCustomerAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
		{
			//var entity = await _unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);

			var entity = await _unitOfWork.CustomerAddressRepository.GetInclude(
				request.CustomerAddressId,
				include: c => c.Include(c => c.Customer));

			if (entity == null) // örnek olsun diye burayı böyle bıraktım yoksa mapp ile yapabilirim
			{
				return new ApiResponse<CustomerAddressResponse>(false)
				{
					IsSuccess = false,
					Message = "Customer Address not found"
				};
			}

			var mapped = _mapper.Map<CustomerAddressResponse>(entity);
			return new ApiResponse<CustomerAddressResponse>(mapped);
		}
	}
}
