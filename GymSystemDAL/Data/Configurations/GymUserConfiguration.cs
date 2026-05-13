using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configurations
{
	internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(U => U.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(U => U.Email).HasColumnType("varchar").HasMaxLength(100).IsRequired();
			builder.Property(U => U.PhoneNumber).HasColumnType("varchar").HasMaxLength(11).IsRequired();

			//Constraint
			builder.ToTable(Tb => {

				Tb.HasCheckConstraint("EmailFormat", "Email like '_%@_%._%'");
				Tb.HasCheckConstraint("PhoneFormat", "PhoneNumber like '01%' and PhoneNumber Not Like '%[^0-9]%' ");


			}
			);  

			builder.HasIndex(U => U.Email).IsUnique();
			builder.HasIndex(U => U.PhoneNumber).IsUnique();
			builder.OwnsOne(U => U.Address, AddressBuilder =>  //NavigationBuilder
			{
				AddressBuilder.Property(Address => Address.City).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30).IsRequired();
				AddressBuilder.Property(Address => Address.Street).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30).IsRequired();
				AddressBuilder.Property(Address => Address.BuildingNo).HasColumnName("BuildingNumber");
			});
		}
	}
}
