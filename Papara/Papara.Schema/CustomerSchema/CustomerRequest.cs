using Papara.Base.Schema;
using Papara.Schema.CustomerAddressSchema;
using Papara.Schema.CustomerDetailSchema;
using Papara.Schema.CustomerPhoneSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Papara.Schema.CustomerSchema
{
	public class CustomerRequest : BaseRequest
	{
		[JsonIgnore]
		public int CustomerNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityNumber { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }


		//public CustomerDetailRequest CustomerDetail { get; set; }
		//public List<CustomerAddressRequest> CustomerAddresses { get; set; }  // hızlı test için kapatıldı
		//public List<CustomerPhoneRequest> CustomerPhones { get; set; }
	}
}
