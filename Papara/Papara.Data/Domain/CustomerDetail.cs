using Papara.Base.Entity;

namespace Papara.Data.Domain
{
	// Id ve IsActive => BaseEntity
	public class CustomerDetail : BaseEntity
	{
		public long CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public string FatherName { get; set; }
		public string MotherName { get; set; }
		public string EducationStatus { get; set; }
		public string MonthlyIncome { get; set; }
		public string Occupation { get; set; }
	}
}
