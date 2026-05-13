using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configurations
{
	internal class PlanConfiguration : IEntityTypeConfiguration<Plan>

	{
		public void Configure(EntityTypeBuilder<Plan> builder)
		{
			builder.Property(P => P.Name).HasColumnType("varchar").HasMaxLength(50);
			builder.Property(P => P.Description).HasColumnType("varchar").HasMaxLength(200);
			//builder.Property(P => P.Price).HasColumnType("decimal(10,2)");
			builder.Property(p => p.Price)
			.HasPrecision(10, 2);
			builder.ToTable(TB => TB.HasCheckConstraint("DurationDaysRange", "DurationDays between 1 and 365"));
		}
	}
}
