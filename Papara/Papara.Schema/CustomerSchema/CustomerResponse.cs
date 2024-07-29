using Papara.Base.Schema;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerPhoneSchema;

namespace Papara.Schema.CustomerSchema
{
	public class CustomerResponse : BaseResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityNumber { get; set; }
		public string Email { get; set; }
		public int CustomerNumber { get; set; }
		public DateTime DateOfBirth { get; set; }

		public CustomerDetailRequest CustomerDetail { get; set; }
		public List<CustomerAddressRequest> CustomerAddresses { get; set; }
		public List<CustomerPhoneRequest> CustomerPhones { get; set; }

	}
}
