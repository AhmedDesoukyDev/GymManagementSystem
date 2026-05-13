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
	internal class MembershipConfiguration : IEntityTypeConfiguration<MemberShip>
	{
		public void Configure(EntityTypeBuilder<MemberShip> builder)
		{
			builder.Property(X=>X.CreatedAt).HasColumnName("StartDate").
				HasDefaultValueSql("GetDate()");
			builder.HasKey(X => new { X.MemberId, X.PlanId });
			builder.Ignore(X => X.Id);
		}
	}
}
