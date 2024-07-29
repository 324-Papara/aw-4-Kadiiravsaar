using Papara.Base.Schema;
using Papara.Schema.CustomerSchema;

namespace Papara.Schema.CustomerDetailSchema
{
	public class CustomerDetailResponseWithInclude : BaseResponse
	{
		public string FatherName { get; set; }
		public string MotherName { get; set; }
		public string EducationStatus { get; set; }
		public string MonthlyIncome { get; set; }
		public string Occupation { get; set; }
		public CustomerResponse Customer { get; set; }

	}
}

