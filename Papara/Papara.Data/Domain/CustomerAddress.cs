﻿using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Domain
{
	// Id ve IsActive => BaseEntity
	public class CustomerAddress : BaseEntity
	{
		public long CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public string Country { get; set; }
		public string City { get; set; }
		public string AddressLine { get; set; }
		public string ZipCode { get; set; }
		public bool IsDefault { get; set; }
	}
}
