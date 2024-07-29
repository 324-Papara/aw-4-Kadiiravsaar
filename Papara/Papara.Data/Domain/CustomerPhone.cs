using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Domain
{
	// Id ve IsActive => BaseEntity
	public class CustomerPhone : BaseEntity
	{
		public long CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public string CountryCode { get; set; } // TUR
		public string Phone { get; set; }
		public bool IsDefault { get; set; }
	}
}
