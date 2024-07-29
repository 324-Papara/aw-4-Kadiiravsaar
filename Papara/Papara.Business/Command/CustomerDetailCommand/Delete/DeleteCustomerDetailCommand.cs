using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;


namespace Papara.Business.Command.CustomerDetailCommand.Delete
{

	public record DeleteCustomerDetailCommand(long CustomerDetailId) : IRequest<ApiResponse>;

	public class DeleteCustomerDetailCommandHandler : IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteCustomerDetailCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
		{
			await _unitOfWork.CustomerDetailRepository.Delete(request.CustomerDetailId);
			await _unitOfWork.Complete();
			return new ApiResponse();
		}
	}
}
