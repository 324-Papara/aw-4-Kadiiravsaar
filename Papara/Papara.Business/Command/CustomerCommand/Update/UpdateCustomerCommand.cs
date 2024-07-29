using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;


namespace Papara.Business.Command.CustomerCommand.Update
{
	public record UpdateCustomerCommand(long CustomerId, CustomerRequest Request) : IRequest<ApiResponse>;

	//public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse>
	//{
	//	private readonly IUnitOfWork _unitOfWork;
	//	private readonly IMapper _mapper;

	//	public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	//	{
	//		_unitOfWork = unitOfWork;
	//		_mapper = mapper;
	//	}
	//	public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
	//	{
	//		var mapped = _mapper.Map<CustomerRequest, Customer>(request.Request);
	//		mapped.Id = request.CustomerId;
	//		_unitOfWork.CustomerRepository.Update(mapped);
	//		await _unitOfWork.Complete();
	//		return new ApiResponse();
	//	}
	//}

	public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			var existingCustomer = await _unitOfWork.CustomerRepository.GetById(request.CustomerId);
			var mapped = _mapper.Map(request.Request, existingCustomer);
			_unitOfWork.CustomerRepository.Update(mapped);
			await _unitOfWork.Complete();

			return new ApiResponse();

		}
	}

}
