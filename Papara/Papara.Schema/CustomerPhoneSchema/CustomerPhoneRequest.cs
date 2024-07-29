using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.CustomerPhoneSchema
{
	public class CustomerPhoneRequest : BaseRequest
	{
		public long CustomerId { get; set; }
		public string CountryCode { get; set; }
		public string Phone { get; set; }
		public bool IsDefault { get; set; }
	}
}
