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
	internal class MemberSessionsConfiguration : IEntityTypeConfiguration<MemberSessions>
	{
		public void Configure(EntityTypeBuilder<MemberSessions> builder)
		{

			builder.Property(X => X.CreatedAt).HasColumnName("BookingDate").
				HasDefaultValueSql("GetDate()");
			builder.HasKey(X => new { X.MemberId, X.SessionId });
			builder.Ignore(X => X.Id);
		}
	}
}
