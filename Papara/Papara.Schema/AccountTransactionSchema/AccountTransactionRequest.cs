using Papara.Base.Schema;

namespace Papara.Schema.AccountTransactionSchema
{
	public class AccountTransactionRequest : BaseRequest
	{
		public long AccountId { get; set; }
		public string ReferenceNumber { get; set; }
		public decimal DebitAmount { get; set; }
		public decimal CreditAmount { get; set; }
		public string Description { get; set; }
		public DateTime TransactionDate { get; set; }
		public string TransactionCode { get; set; }
	}
}
