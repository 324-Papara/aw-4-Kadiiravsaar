using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Domain
{
	// Id ve IsActive => BaseEntity
	public class Customer : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityNumber { get; set; }
		public string Email { get; set; }
		public int CustomerNumber { get; set; }
		public DateTime DateOfBirth { get; set; }


		public virtual User User { get; set; }

		public virtual CustomerDetail CustomerDetail { get; set; }
		public virtual List<CustomerAddress> CustomerAddresses { get; set; }
		public virtual List<CustomerPhone> CustomerPhones { get; set; }

		public virtual List<Account> Accounts { get; set; }

	}

}
