using Application.Repository;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using RedSocial.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Persistence.Repositories
{
    public class ComentarioRepository : GenericRepository<Comentario>, IComentarioRepository
    {
        private readonly DbContextMarket _dbContext;
        public ComentarioRepository(DbContextMarket dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }
    }
}
