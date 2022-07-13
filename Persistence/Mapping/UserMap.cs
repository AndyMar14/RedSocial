using RedSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RedSocial.Persistence.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuarios")
               .HasKey(u => u.Id);

            builder
                .HasMany<Publicacion>(u => u.Publicaciones)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany<Amigos>(u => u.Amigos)
                .WithOne(p => p.Amigo)
                .HasForeignKey(p => p.IdAmigo)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany<Comentario>(u => u.Comentarios)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
