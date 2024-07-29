using Papara.Schema.CustomerSchema;

namespace Papara.Schema.CustomerPhoneSchema
{
	public class CustomerPhoneResponseWithDetail
	{
		public long CustomerId { get; set; }
		public string CountryCode { get; set; }
		public string Phone { get; set; }
		public bool IsDefault { get; set; }

		public CustomerResponse Customer { get; set; }


	}
}
