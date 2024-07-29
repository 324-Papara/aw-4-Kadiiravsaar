using Papara.Base.Schema;

namespace Papara.Schema.CustomerAddressSchema
{
	public class CustomerAddressResponse : BaseResponse
	{
		public long CustomerId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string AddressLine { get; set; }
		public string ZipCode { get; set; }
		public bool IsDefault { get; set; }

		public string CustomerName { get; set; }
		public string CustomerIdentityNumber { get; set; }
		public int CustomerNumber { get; set; }
	}
}
