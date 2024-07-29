using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.CountrySchema
{

	public class CountryRequest : BaseRequest
	{
		public string CountryCode { get; set; }
		public string Name { get; set; }
	}

}
