using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.AccountSchema
{
	public class AccountRequest : BaseRequest
	{
		public long CustomerId { get; set; }
		public string Name { get; set; }
		public string CurrencyCode { get; set; }
	}
}
