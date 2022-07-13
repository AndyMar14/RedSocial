using RedSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RedSocial.Persistence.Mapping
{
    public class PublicacionMap : IEntityTypeConfiguration<Publicacion>
    {
        public void Configure(EntityTypeBuilder<Publicacion> builder)
        {
            builder.ToTable("publicaciones")
               .HasKey(u => u.Id);

            
           builder
            .HasMany<Comentario>(p => p.Comentarios)
            .WithOne(p => p.Publicacion)
            .HasForeignKey(p => p.IdPublicacion)
            .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
