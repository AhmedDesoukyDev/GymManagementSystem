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
	internal class MemberConfiguration :GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
	{
		public new void Configure(EntityTypeBuilder<Member> builder)
		{
			builder.Property(M => M.CreatedAt).HasColumnName("JoinDate").HasDefaultValueSql("GetDate()");
			base.Configure(builder);

		}
	}
}
