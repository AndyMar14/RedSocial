using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Services
{
    public class AmigosService : GenericService<SaveAmigosViewModel, AmigosViewModel, Amigos>,IAmigosService
    {
        private readonly IAmigosRepository _amigosRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public AmigosService(IAmigosRepository amigosRepository, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor) : base(amigosRepository, mapper)
        {
            _amigosRepository = amigosRepository;
            _mapper = mapper;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override async Task<SaveAmigosViewModel> Add(SaveAmigosViewModel vm)
        {
            vm.Estado = 1;

            SaveAmigosViewModel userVm = await base.Add(vm);

            return userVm;
        }
        public async Task<User> GetUserName(SaveAmigosViewModel vm)
        {
            User amigo = await _amigosRepository.GetByNameAsync(vm);

            return amigo;
        }

        public async Task<bool> GetRelationship(int IdUsuario,int IdAmigo)
        {
            Amigos amigo = await _amigosRepository.GetRelationship(IdUsuario,IdAmigo);

            if (amigo == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Amigos> GetRelationshipId(int IdUsuario, int IdAmigo)
        {
            Amigos amigo = await _amigosRepository.GetRelationshipId(IdUsuario, IdAmigo);

            return amigo;
        }
        public async Task<List<AmigosViewModel>> GetAllAmigosWithIncludes()
        {
            var amigosList = await _amigosRepository.GetAllWithIncludeAsync(new List<string> { "Amigo" });

            return amigosList.Where(amigo => amigo.IdUsuario == userViewModel.Id).Select(post => new AmigosViewModel
            {
                Id = post.Id,
                NombreAmigo = post.Amigo.Nombre,
                ApellidoAmigo = post.Amigo.Apellido,
                UsuarioAmigo = post.Amigo.NombreUsuario,
                IdUsuario = userViewModel.Id,
                IdAmigo = post.Amigo.Id
            }).ToList();
        }

        public async Task<List<int>> GetAllAmigosId()
        {
            var amigosList = await _amigosRepository.GetAllWithIncludeAsync(new List<string> { "Amigo" });

            return amigosList.Where(amigo => amigo.IdUsuario == userViewModel.Id).Select(post => post.IdAmigo).ToList();
        }
    }
}
