using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModels.Comentario;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.ViewModels.Comentario;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Services
{
    public class ComentarioService : GenericService<SaveComentarioViewModel, ComentarioViewModel, Comentario>,IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public ComentarioService(IComentarioRepository comentarioRepository, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor) : base(comentarioRepository, mapper)
        {
            _comentarioRepository = comentarioRepository;
            _mapper = mapper;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override async Task<SaveComentarioViewModel> Add(SaveComentarioViewModel vm)
        {
            vm.Fecha = DateTime.Now;
            SaveComentarioViewModel comentarioVm = await base.Add(vm);

            return comentarioVm;
        }
    }
}
