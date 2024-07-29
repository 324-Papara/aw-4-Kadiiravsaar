﻿using Papara.Base.Entity;

namespace Papara.Data.Domain
{
	public class User : BaseEntity
	{
		public long? CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public int Status { get; set; }
	}

}
