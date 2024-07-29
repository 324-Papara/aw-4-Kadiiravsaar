using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;

namespace Papara.Business.Command.CustomerCommand.Create
{
	public record CreateCustomerCommand(CustomerRequest Request) : IRequest<ApiResponse<CustomerResponse>>;

	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			var mapped = _mapper.Map<CustomerRequest, Customer>(request.Request);
			mapped.CustomerNumber = new Random().Next(1000000, 9999999);
			await _unitOfWork.CustomerRepository.Insert(mapped);
			await _unitOfWork.Complete();

			var response = _mapper.Map<CustomerResponse>(mapped);
			return new ApiResponse<CustomerResponse>(response);
		}
	}
}
