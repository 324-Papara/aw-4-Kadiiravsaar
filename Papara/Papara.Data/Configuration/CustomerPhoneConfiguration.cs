﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.Data.Domain;

namespace Papara.Data.Configuration
{
	public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
	{
		public void Configure(EntityTypeBuilder<CustomerPhone> builder)
		{

			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.IsActive).IsRequired(true);
			builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

			builder.Property(x => x.CustomerId).IsRequired(true);
			builder.Property(x => x.CountryCode).IsRequired(true).HasMaxLength(3);
			builder.Property(x => x.Phone).IsRequired(true).HasMaxLength(10);
			builder.Property(x => x.IsDefault).IsRequired(true);

			builder.HasIndex(x => new { x.CountryCode, x.Phone }).IsUnique(true);
		}
	}
	
		
		
}
