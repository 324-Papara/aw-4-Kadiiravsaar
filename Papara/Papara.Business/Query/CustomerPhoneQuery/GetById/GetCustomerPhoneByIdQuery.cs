using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Business.Query.CustomerQuery.GetById;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerPhoneSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerPhoneQuery.GetById
{

	public record GetCustomerPhoneByIdQuery(long CustomerPhoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>;

	public class GetCustomerPhoneByIdQueryHandler : IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCustomerPhoneByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
		{
			//var entity = await _unitOfWork.CustomerPhoneRepository.GetById(request.CustomerPhoneId);

			var entity = await _unitOfWork.CustomerPhoneRepository.GetInclude(
				request.CustomerPhoneId,
				include: c => c.Include(c => c.Customer));

			if (entity == null)
			{
				return new ApiResponse<CustomerPhoneResponse>(false)
				{
					IsSuccess = false,
					Message = "Customer Phone  not found"
				};
			}

			var mapped = _mapper.Map<CustomerPhoneResponse>(entity);
			return new ApiResponse<CustomerPhoneResponse>(mapped);
		}
	}
}
