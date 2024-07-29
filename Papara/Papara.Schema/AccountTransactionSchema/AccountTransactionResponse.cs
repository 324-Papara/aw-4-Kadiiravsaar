using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.AccountTransactionSchema
{


	public class AccountTransactionResponse : BaseResponse
	{
		public long AccountId { get; set; }
		public string ReferenceNumber { get; set; }
		public decimal DebitAmount { get; set; }
		public decimal CreditAmount { get; set; }
		public string Description { get; set; }
		public DateTime TransactionDate { get; set; }
		public string TransactionCode { get; set; }

		public string CustomerName { get; set; }
		public string CustomerIdentityNumber { get; set; }
		public int CustomerNumber { get; set; }
	}
}
