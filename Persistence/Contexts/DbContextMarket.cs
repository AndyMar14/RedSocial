using RedSocial.Domain.Entities;
using RedSocial.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace RedSocial.Persistence.Contexts
{
    public class DbContextMarket : DbContext
    {
        public DbContextMarket(DbContextOptions<DbContextMarket> options) : base(options){}
        public DbSet<User> User { get; set; }
        public DbSet<Publicacion> publicacion { get; set; }
        public DbSet<Amigos> amigos { get; set; }

        public DbSet<Comentario> comentario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new PublicacionMap());
            builder.ApplyConfiguration(new AmigosMap());
            builder.ApplyConfiguration(new ComentarioMap());
        }
    }
}
