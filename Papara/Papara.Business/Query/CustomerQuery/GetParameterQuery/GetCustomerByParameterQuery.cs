using AutoMapper;
using LinqKit;
using MediatR;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerQuery.GetParameterQuery
{
	public record GetCustomerByParameterQuery(long? CustomerNumber, string FirstName, string LastName, string IdentityNumber) : IRequest<ApiResponse<List<CustomerResponse>>>;


	public class GetCustomerByParameterQueryHandler : IRequestHandler<GetCustomerByParameterQuery, ApiResponse<List<CustomerResponse>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetCustomerByParameterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParameterQuery request, CancellationToken cancellationToken)
		{



			var predicate = PredicateBuilder.New<Customer>(true);
			if (request.CustomerNumber > 0)
				predicate.And(x => x.CustomerNumber == request.CustomerNumber);
			if (!string.IsNullOrEmpty(request.FirstName))
				predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
			if (!string.IsNullOrEmpty(request.LastName))
				predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
			if (!string.IsNullOrEmpty(request.IdentityNumber))
				predicate.And(x => x.IdentityNumber.ToUpper().Contains(request.IdentityNumber.ToUpper()));

			List<Customer> entityList = await _unitOfWork.CustomerRepository.Where(predicate, "CustomerDetail");

			var mappedList = _mapper.Map<List<CustomerResponse>>(entityList);
			return new ApiResponse<List<CustomerResponse>>(mappedList);



			//var customers = await _unitOfWork.CustomerRepository
			//  .Where(c => (c.FirstName == request.Name || string.IsNullOrEmpty(request.Name)));

			//var customerResponses = _mapper.Map<List<CustomerResponse>>(customers);

			//return new ApiResponse<List<CustomerResponse>>(customerResponses);

		}
	}
}
