using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Application.Helpers;
using RedSocial.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using RedSocial.Core.Application.ViewModels.Comentario;

namespace Application.Services
{
    public class PublicacionService : GenericService<SavePublicacionViewModel,PublicacionViewModel,Publicacion>,IPublicacionService
    {
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IMapper _mapper;
        private readonly UserViewModel userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PublicacionService(IPublicacionRepository publicacionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(publicacionRepository, mapper)
        {
            _publicacionRepository = publicacionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        
        }

        public override async Task<SavePublicacionViewModel> Add(SavePublicacionViewModel vm)
        {
            vm.IdUsuario = userViewModel.Id;
            vm.Fecha = DateTime.Now;
            SavePublicacionViewModel publicacionVm = await base.Add(vm);

            return publicacionVm;
        }

        public override async Task Update(SavePublicacionViewModel vm,int Id)
        {
            vm.IdUsuario = userViewModel.Id;
            vm.Fecha = DateTime.Now;
            await base.Update(vm,Id);
        }
        public async Task<List<PublicacionViewModel>> GetAllMineViewModel()
        {
            var publicacionList = await _publicacionRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comentarios" });

            return publicacionList.Where(post => post.IdUsuario == userViewModel.Id).Select(post => new PublicacionViewModel
            {
                Id = post.Id,
                Detalle = post.Detalle,
                Imagen = post.Imagen,
                Fecha = post.Fecha,
                NombreUsuario = post.User.NombreUsuario,
                FotoUsuario = post.User.Foto,
                Comentarios = post.Comentarios.Select(coment => new ComentarioViewModel
                {
                    Id = coment.Id,
                    Detalle = coment.Detalle,
                    Fecha = coment.Fecha,
                    User = new UserViewModel
                    {
                        NombreUsuario = coment.User.NombreUsuario,
                        Foto = coment.User.Foto
                    }
                }).ToList()
            }).OrderByDescending(post => post.Id)
            .ToList();
        }

        public async Task<List<PublicacionViewModel>> GetAllFriendsViewModel(List<int> amigosList)
        {
            var publicacionList = await _publicacionRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comentarios"});

            return publicacionList.Where(post => amigosList.Contains(post.IdUsuario)).Select(post => new PublicacionViewModel
            {
                Id = post.Id,
                Detalle = post.Detalle,
                Imagen = post.Imagen,
                Fecha = post.Fecha,
                NombreUsuario = post.User.NombreUsuario,
                FotoUsuario = post.User.Foto,
                Comentarios = post.Comentarios.Select(coment => new ComentarioViewModel { 
                    Id = coment.Id,
                    Detalle = coment.Detalle,
                    Fecha = coment.Fecha,
                    User = new UserViewModel
                    {
                        NombreUsuario = coment.User.NombreUsuario,
                        Foto = coment.User.Foto
                    }
                }).ToList()
            }).OrderByDescending(post => post.Id)
            .ToList();
        }
    }
}
