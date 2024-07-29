using MediatR;
using Papara.Base.Response;
using Papara.Business.Command.CustomerCommand.Delete;
using Papara.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerPhoneCommand.Delete
{
	public record DeleteCustomerPhoneCommand(long CustomerPhoneId) : IRequest<ApiResponse>;

	public class DeleteCustomerPhoneCommandHandler : IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteCustomerPhoneCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
		{
			await _unitOfWork.CustomerPhoneRepository.Delete(request.CustomerPhoneId);
			await _unitOfWork.Complete();
			return new ApiResponse();
		}
	}
}
