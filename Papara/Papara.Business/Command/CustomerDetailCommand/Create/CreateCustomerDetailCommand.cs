using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Business.Command.CustomerCommand.Create;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command.CustomerDetailCommand.Create
{

	public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;

	public class CreateCustomerDetailCommandHandler : IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateCustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
		{

			var customer = await _unitOfWork.CustomerRepository.GetInclude(request.Request.CustomerId,
				include: c => c.Include(c => c.CustomerDetail));
			

			// Bu kodlar bir iş kuralı sınıfına atılıp oradan çekilebilir

				// Eğer customer yok ise hata fırlatalım
			if (customer == null)
			{
				return new ApiResponse<CustomerDetailResponse>("Customer not found.");
			}

			// Customer'ın zaten bir detayı var ise hata fırlatalım bu hatalar sabir bir yerden gelmeli !!!!
			if (customer.CustomerDetail != null)
			{
				return new ApiResponse<CustomerDetailResponse>("Customer already has a detail.");
			}


			var mapped = _mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
			await _unitOfWork.CustomerDetailRepository.Insert(mapped);
			await _unitOfWork.Complete();

			var response = _mapper.Map<CustomerDetailResponse>(mapped);
			return new ApiResponse<CustomerDetailResponse>(response);
		}
	}
}
