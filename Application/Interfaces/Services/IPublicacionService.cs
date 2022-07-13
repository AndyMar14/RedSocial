using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IPublicacionService : IGenericService<SavePublicacionViewModel, PublicacionViewModel, Publicacion>
    {
        Task<List<PublicacionViewModel>> GetAllMineViewModel();
        Task<List<PublicacionViewModel>> GetAllFriendsViewModel(List<int> amigosList);
    }
}
