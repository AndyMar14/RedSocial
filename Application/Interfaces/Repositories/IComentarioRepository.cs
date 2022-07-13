using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Repositories
{
    public interface IComentarioRepository : IGenericRepository<Comentario>
    {
    }
}
