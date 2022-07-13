using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using RedSocial.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class PublicacionRepository : GenericRepository<Publicacion>, IPublicacionRepository
    {
        private readonly DbContextMarket _dbContext;

        public PublicacionRepository(DbContextMarket dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
