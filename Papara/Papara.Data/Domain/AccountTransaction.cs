using Papara.Base.Entity;

namespace Papara.Data.Domain
{
	public class AccountTransaction : BaseEntity
	{
		public long AccountId { get; set; }
		public virtual Account Account { get; set; }

		public string ReferenceNumber { get; set; }
		public decimal DebitAmount { get; set; }
		public decimal CreditAmount { get; set; }
		public string Description { get; set; }
		public DateTime TransactionDate { get; set; }
		public string TransactionCode { get; set; }
	}

}
