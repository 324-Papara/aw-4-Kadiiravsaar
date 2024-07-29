using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerAddressCommand.Create
{
	public record CreateCustomerAddressCommand(CustomerAddressRequest request) : IRequest<ApiResponse<CustomerAddressResponse>>;

	public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
		{
			var mapped = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.request);
			await _unitOfWork.CustomerAddressRepository.Insert(mapped);
			await _unitOfWork.Complete();
				
			var response = _mapper.Map<CustomerAddressResponse>(mapped);
			return new ApiResponse<CustomerAddressResponse>(response);
		}
	}
}
