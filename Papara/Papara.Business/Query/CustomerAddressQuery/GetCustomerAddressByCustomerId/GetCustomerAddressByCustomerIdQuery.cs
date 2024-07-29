using MediatR;
using Papara.Base.Response;
using Papara.Schema.CustomerAddressSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query.CustomerAddressQuery.GetCustomerAddressByCustomerId
{
	public record GetCustomerAddressByCustomerIdQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;

}
