﻿using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.CustomerAddressSchema
{
	public class CustomerAddressRequest : BaseRequest
	{
		public long CustomerId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string AddressLine { get; set; }
		public string ZipCode { get; set; }
		public bool IsDefault { get; set; }
	}
}
