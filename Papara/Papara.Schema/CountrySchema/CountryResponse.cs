using Papara.Base.Schema;

namespace Papara.Schema.CountrySchema
{
	public class CountryResponse : BaseResponse
	{
		public string CountyCode { get; set; }
		public string Name { get; set; }
	}

}
