using MediatR;
using Papara.Base.Response;
using Papara.Business.Command.CustomerCommand.Delete;
using Papara.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerAddressCommand.Delete
{

	public record DeleteCustomerAddressCommand(long CustomerAddresId) : IRequest<ApiResponse>;


	public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
		{
			await _unitOfWork.CustomerAddressRepository.Delete(request.CustomerAddresId);
			await _unitOfWork.Complete();
			return new ApiResponse();
		}
	}
}
