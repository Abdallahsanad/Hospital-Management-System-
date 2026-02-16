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
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        void IEntityTypeConfiguration<Medicine>.Configure(EntityTypeBuilder<Medicine> builder)
        {
            

            builder.Property(x => x.Price)
                .HasColumnType("money");
        }
    }
}
