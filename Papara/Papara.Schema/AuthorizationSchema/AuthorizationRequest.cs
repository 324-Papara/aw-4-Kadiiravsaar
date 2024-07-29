using Papara.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.AuthorizationSchema
{
	public class AuthorizationRequest : BaseRequest
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
