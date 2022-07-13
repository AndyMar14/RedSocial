using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
        Task<User> GetByNameAsync(string NombreUsuario);
        Task UpdatePassword(User entity, int Id);
    }
}
