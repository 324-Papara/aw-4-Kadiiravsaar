using Papara.Base.Schema;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerPhoneSchema;

namespace Papara.Schema.CustomerSchema
{
	public class CustomerResponseWithDetail: BaseResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityNumber { get; set; }
		public string Email { get; set; }
		public int CustomerNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public virtual CustomerDetailResponse CustomerDetail { get; set; }
		public virtual List<CustomerPhoneResponse> CustomerPhones { get; set; }
		public virtual List<CustomerAddressResponse> CustomerAddresses { get; set; }
	}
}
