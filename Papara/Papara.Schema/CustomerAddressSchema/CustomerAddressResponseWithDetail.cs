using Papara.Base.Schema;
using Papara.Schema.CustomerSchema;

namespace Papara.Schema.CustomerAddressSchema
{
	public class CustomerAddressResponseWithDetail : BaseResponse
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string AddressLine { get; set; }
		public string ZipCode { get; set; }
		public bool IsDefault { get; set; }
		public CustomerResponse Customer { get; set; }

	}

}
