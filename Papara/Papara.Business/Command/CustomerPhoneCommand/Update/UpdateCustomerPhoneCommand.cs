using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerPhoneSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerPhoneCommand.Update
{
	public record UpdateCustomerPhoneCommand(long CustomerPhoneId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;

	public class UpdateCustomerPhoneCommandHandler : IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
		{
			var existingCustomer = await _unitOfWork.CustomerPhoneRepository.GetById(request.CustomerPhoneId);
			var mapped = _mapper.Map(request.Request, existingCustomer);
			_unitOfWork.CustomerPhoneRepository.Update(mapped);
			await _unitOfWork.Complete();

			return new ApiResponse();

		}
	}
}
