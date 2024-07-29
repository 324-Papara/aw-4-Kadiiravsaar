using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Command.CustomerCommand.Create;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerPhoneSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerPhoneCommand.Create
{

	public record CreateCustomerPhoneCommand(CustomerPhoneRequest  Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;

	public class CreateCustomerPhoneCommandHandler : IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
		{
			var mapped = _mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
			await _unitOfWork.CustomerPhoneRepository.Insert(mapped);
			await _unitOfWork.Complete();

			var response = _mapper.Map<CustomerPhoneResponse>(mapped);
			return new ApiResponse<CustomerPhoneResponse>(response);
		}
	}
}
