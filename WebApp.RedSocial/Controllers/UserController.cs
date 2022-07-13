using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RedSocial.Core.Application.Helpers;
using RedSocial.RedSocial.Middleware;
using AutoMapper;
using RedSocial.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace RedSocial.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,ValidateUserSession validateUserSession, IMapper mapper)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            ViewBag.Mensaje = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            UserViewModel userVm = await _userService.Login(vm);

            if (userVm != null)
            {
                if (userVm.Estado == 0)
                {
                    ModelState.AddModelError("userValidation", "El usuario esta Inactivo, verifica tu correo");
                    return View(vm);
                }

                HttpContext.Session.Set<UserViewModel>("user",userVm);
                return RedirectToRoute(new {controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation","Datos incorrectos");
            }
            return View(vm);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public IActionResult Create()
        {
            ViewBag.Existe = false;
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SaveUser", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {

            var userCreated = await _userService.GetUserName(vm.NombreUsuario);
            if (!ModelState.IsValid || userCreated != null)
            {
                ViewBag.Existe = userCreated != null ? true : false;
                return View("SaveUser", vm);
            }
            ViewBag.Existe = false;

            SaveUserViewModel userVm = await _userService.Add(vm);

            if (userVm != null && userVm.Id != 0)
            {
                userVm.Foto = UploadFile(vm.FileFoto, userVm.Id);
                await _userService.Update(userVm, userVm.Id);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> RestablecerClave(LoginViewModel vm)
        {
            var userCreated = await _userService.GetUserName(vm.Nombre);
            if (!ModelState.IsValid || userCreated == null)
            {
                ModelState.AddModelError("userValidation", "El usuario no existe");
                return View("Index");
            }
            ViewBag.Mensaje = "Clave Actualizada, verifica tu correo";
            await _userService.UpateClave(vm);
            return View("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveUser", await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            await _userService.Update(vm,vm.Id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            await _userService.Delete(id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        [HttpGet]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var user = await _userService.GetByIdSaveViewModel(id);
            SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(user);
            vm.Estado = 1;
            await _userService.Update(vm,id);
            return View("ActivateUser", vm);
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }
            string basePath = $"/Images/Usuarios/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);

            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePath = imageUrl.Split("/");
                string oldImageName = oldImagePath[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);
                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
