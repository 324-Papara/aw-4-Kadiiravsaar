using Papara.Base.Entity;

namespace Papara.Data.Domain
{
	public class Account : BaseEntity
	{
		public long CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public string Name { get; set; }
		public int AccountNumber { get; set; }
		public string IBAN { get; set; }
		public decimal Balance { get; set; }
		public string CurrencyCode { get; set; }
		public DateTime OpenDate { get; set; }

		public virtual List<AccountTransaction> AccountTransactions { get; set; }
	}

}
