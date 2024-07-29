using Papara.Base.Entity;

namespace Papara.Data.Domain
{
	public class Country : BaseEntity
	{
		public string CountryCode { get; set; }
		public string Name { get; set; }
	}

}
