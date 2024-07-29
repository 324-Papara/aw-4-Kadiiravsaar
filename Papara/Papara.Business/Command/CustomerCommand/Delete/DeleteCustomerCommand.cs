using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerCommand.Delete
{
	public record DeleteCustomerCommand(long CustomerId) : IRequest<ApiResponse>;

	public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
		{
			await _unitOfWork.CustomerRepository.Delete(request.CustomerId);
			await _unitOfWork.Complete();
			return new ApiResponse();
		}
	}
}
