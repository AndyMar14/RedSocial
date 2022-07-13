using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<User> GetUserName(string NombreUsuario);
        Task<User> UpateClave(LoginViewModel vm);
    }
}
