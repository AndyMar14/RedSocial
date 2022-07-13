using RedSocial.Domain.Entities;
using RedSocial.Core.Application.Interfaces.Repositories;
using System.Threading.Tasks;
using RedSocial.Core.Application.ViewModels.User;
using AutoMapper;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.Dtos.Email;

namespace Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService) : base(userRepository,mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            vm.Estado = 0;
            SaveUserViewModel userVm = await base.Add(vm);

            await _emailService.SendAsync(new EmailRequest
            {
                To = userVm.Correo,
                From = _emailService._mailSettings.EmailFrom,
                Body = $"Se ha creado el usuario: {userVm.NombreUsuario} <br> Para Activarlo Haga <a href='https://localhost:44363/User/ActivateUser/{userVm.Id}'>Click Aqui</a>",
                Subject = "Creacion de usuario"
            });

            return userVm;
        }
        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            User user = await _userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userVm = _mapper.Map<UserViewModel>(user);

            return userVm;
        }

        public async Task<User> GetUserName(string NombreUsuario)
        {
            User user = await _userRepository.GetByNameAsync(NombreUsuario);

            return user;
        }

        public async Task<User> UpateClave(LoginViewModel vm)
        {
            User userVm = await _userRepository.GetByNameAsync(vm.Nombre);
            userVm.Password = vm.Password;
            await _userRepository.UpdatePassword(userVm, userVm.Id);

            await _emailService.SendAsync(new EmailRequest
            {
                To = userVm.Correo,
                From = _emailService._mailSettings.EmailFrom,
                Body = $"Se ha Cambiado la clave de tu usuario {userVm.NombreUsuario}:  <br> La nueva clave es: <strong>{vm.Password}<strong>",
                Subject = "Cambio de clave"
            });
            return userVm;
        }

    }
}
