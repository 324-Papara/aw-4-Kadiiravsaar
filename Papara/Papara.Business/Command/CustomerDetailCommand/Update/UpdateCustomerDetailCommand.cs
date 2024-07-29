using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;


namespace Papara.Business.Command.CustomerDetailCommand.Update
{
	public record UpdateCustomerDetailCommand(long CustomerDetailId, CustomerDetailRequest Request) : IRequest<ApiResponse>;

	public class UpdateCustomerDetailCommandHandler : IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateCustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
		{
			var existingCustomerDetail = await _unitOfWork.CustomerDetailRepository.GetById(request.CustomerDetailId);
			var mapped = _mapper.Map(request.Request, existingCustomerDetail);
			_unitOfWork.CustomerDetailRepository.Update(mapped);
			await _unitOfWork.Complete();

			return new ApiResponse();

		}
	}

}
