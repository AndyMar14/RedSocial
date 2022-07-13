using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IAmigosService : IGenericService<SaveAmigosViewModel, AmigosViewModel, Amigos>
    {
        Task<User> GetUserName(SaveAmigosViewModel vm);
        Task<List<AmigosViewModel>> GetAllAmigosWithIncludes();
        Task<bool> GetRelationship(int IdUsuario, int IdAmigo);
        Task<List<int>> GetAllAmigosId();
        Task<Amigos> GetRelationshipId(int IdUsuario, int IdAmigo);
    }
}
