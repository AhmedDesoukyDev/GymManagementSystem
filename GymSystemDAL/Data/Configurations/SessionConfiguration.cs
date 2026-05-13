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
	internal class SessionConfiguration : IEntityTypeConfiguration<Session>
	{
		public void Configure(EntityTypeBuilder<Session> builder)
		{
			builder.ToTable(TB =>
			{

				TB.HasCheckConstraint("CapacityRange", "Capacity Between 1 And 25");
				TB.HasCheckConstraint("DateConstraint", "EndDate > StartDate");



			});

			//Not Needed in case theres no another navigational property
			builder.HasOne(S => S.SessionCategory)
				.WithMany(C => C.Sessions)
				.HasForeignKey(S => S.CategoryId);

			builder.HasOne(S => S.SessionTrainer).
				WithMany(C => C.Sessions).
				HasForeignKey(S => S.TrainerId);

		}
	}
}