using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.CustomerDetailSchema
{
	public class CustomerDetailRequest : BaseRequest
	{
		public int CustomerId { get; set; }
		public string FatherName { get; set; }
		public string MotherName { get; set; }
		public string EducationStatus { get; set; }
		public string MonthlyIncome { get; set; }
		public string Occupation { get; set; }
	}
}
