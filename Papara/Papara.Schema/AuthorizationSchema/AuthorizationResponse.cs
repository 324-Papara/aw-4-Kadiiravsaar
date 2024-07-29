using Papara.Base.Schema;

namespace Papara.Schema.AuthorizationSchema
{
	public class AuthorizationResponse : BaseResponse
	{
		public DateTime ExpireTime { get; set; }
		public string AccessToken { get; set; }
		public string UserName { get; set; }
	}
}
