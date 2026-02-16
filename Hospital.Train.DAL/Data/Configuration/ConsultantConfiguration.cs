using Hospital.Train.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.DAL.Data.Configuration
{
    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
    {
        void IEntityTypeConfiguration<Consultant>.Configure(EntityTypeBuilder<Consultant> builder)
        {
            builder.Property(x => x.Salary)
                .HasColumnType("money");

            builder.Property(c => c.Address)
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasMaxLength(50);

            builder.Property(c => c.Phone)
                .HasMaxLength(15);
        }
    }
}
