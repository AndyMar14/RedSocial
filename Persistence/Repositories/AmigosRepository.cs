using Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Domain.Entities;
using RedSocial.Persistence.Contexts;
using System.Threading.Tasks;

namespace RedSocial.Persistence.Repositories
{
    public class AmigosRepository : GenericRepository<Amigos>,IAmigosRepository
    {
        private readonly DbContextMarket _dbContext;

        public AmigosRepository(DbContextMarket dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<User> GetByNameAsync(SaveAmigosViewModel amigoVm)
        {
            User amigo = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(amigo => amigo.NombreUsuario == amigoVm.NombreAmigo && amigo.Id != amigoVm.IdUsuario);
            return amigo;
        }

        public async Task<Amigos> GetRelationship(int IdUsuario, int IdAmigo)
        {
            Amigos amistad = await _dbContext.Set<Amigos>()
                .FirstOrDefaultAsync(a=> a.IdAmigo == IdAmigo && a.IdUsuario == IdUsuario);

            return amistad;
        }
        public async Task<Amigos> GetRelationshipId(int IdUsuario, int IdAmigo)
        {
            Amigos amistad = await _dbContext.Set<Amigos>()
                .FirstOrDefaultAsync(a => a.IdAmigo == IdAmigo && a.IdUsuario == IdUsuario);
            return amistad;
        }
    }
}
