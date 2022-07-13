using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Persistence.Mapping
{
    public class AmigosMap : IEntityTypeConfiguration<Amigos>
    {
        public void Configure(EntityTypeBuilder<Amigos> builder)
        {
            builder.ToTable("amigos")
               .HasKey(u => u.Id);
        }
    }
}
