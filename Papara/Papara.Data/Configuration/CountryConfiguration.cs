using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.Data.Domain;

namespace Papara.Data.Configuration
{
	public class CountryConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.IsActive).IsRequired(true);
			builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

			builder.Property(x => x.CountryCode).IsRequired(true).HasMaxLength(3);
			builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);

			builder.HasIndex(x => new { x.CountryCode }).IsUnique(true);
		}
	}



}
