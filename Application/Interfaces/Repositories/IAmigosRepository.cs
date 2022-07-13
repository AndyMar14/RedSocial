using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Repositories
{
    public interface IAmigosRepository : IGenericRepository<Amigos>
    {
        Task<User> GetByNameAsync(SaveAmigosViewModel amigoVm);
        Task<Amigos> GetRelationship(int IdUsuario, int IdAmigo);
        Task<Amigos> GetRelationshipId(int IdUsuario, int IdAmigo);
    }
}
