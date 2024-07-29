using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Data.Domain;
using Papara.Data.UnitOfWork;
using Papara.Schema.CustomerSchema;

namespace Papara.Business.Query.CustomerQuery.GetListWithInclude
{
	public record GetListCustomersWithDetailQuery() : IRequest<ApiResponse<List<CustomerResponseWithDetail>>>;
	public class GetListCustomersWithDetailQueryHandler : IRequestHandler<GetListCustomersWithDetailQuery, ApiResponse<List<CustomerResponseWithDetail>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetListCustomersWithDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
	

		public async Task<ApiResponse<List<CustomerResponseWithDetail>>> Handle(GetListCustomersWithDetailQuery request, CancellationToken cancellationToken)
		{

			List<Customer> entityList = await _unitOfWork.CustomerRepository.GetAllInclude(
		   include: c => c
			   .Include(c => c.CustomerDetail)
			   .Include(c => c.CustomerPhones)
			   .Include(c => c.CustomerAddresses),

		   predicate: x => x.IsActive == true // Sadece aktif kayıtları getirmek için predicate
	   );

			// İlişkili varlıkları filtreleme yaparak isactive alanını ture olanları getiriyorum CustomerDetail bir nesne olduğu için where kullanamıyorum where sadece koleksiyon varlıklarda kullandım
			foreach (var customer in entityList)
			{
				customer.CustomerDetail = customer.CustomerDetail?.IsActive == true ? customer.CustomerDetail : null;
				customer.CustomerPhones = customer.CustomerPhones?.Where(phone => phone.IsActive).ToList();
			}


			var mappedList = _mapper.Map<List<CustomerResponseWithDetail>>(entityList);
			return new ApiResponse<List<CustomerResponseWithDetail>>(mappedList);
		}
	}

}