using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerAddressSchema;


namespace Papara.Business.Command.CustomerAddressCommand.Update
{
	public record UpdateCustomerAddressCommand(long CustomerAddresId, CustomerAddressRequest Request) : IRequest<ApiResponse>;

	public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
		{
			var existingCustomerAddress = await _unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddresId);
			var mapped = _mapper.Map(request.Request, existingCustomerAddress);
			_unitOfWork.CustomerAddressRepository.Update(mapped);
			await _unitOfWork.Complete();

			return new ApiResponse();

		}
	}
}
