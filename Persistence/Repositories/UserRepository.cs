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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbContextMarket _dbContext;

        public UserRepository(DbContextMarket dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            return await base.AddAsync(entity);
        }
        public override async Task UpdateAsync(User entity,int Id)
        {
            await base.UpdateAsync(entity, Id);
        }

        public async Task UpdatePassword(User entity, int Id)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.UpdateAsync(entity, Id);
        }
        public async Task<User> GetByNameAsync(string NombreUsuario)
        {
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.NombreUsuario == NombreUsuario);
            return user;
        }
        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypted = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.NombreUsuario == loginVm.Nombre && user.Password == passwordEncrypted);
            return user;
        }
    }
}
