using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.RedSocial.Middleware;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.RedSocial.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System;
using RedSocial.Application.ViewModels.Comentario;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Application.Interfaces.Services;

namespace WebApp.RedSocial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<AmigosController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicacionService _publicacionService;
        private readonly IComentarioService _comentarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public HomeController(ILogger<AmigosController> logger, ValidateUserSession validateUserSession, IPublicacionService publicacionService, IHttpContextAccessor httpContextAccessor, IComentarioService comentarioService)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _publicacionService = publicacionService;
            _comentarioService = comentarioService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Publicaciones = await _publicacionService.GetAllMineViewModel();
            return View("Index", new SavePublicacionViewModel());
        }
        public async Task<IActionResult> IndexEdit(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Publicaciones = await _publicacionService.GetAllMineViewModel();
            return View("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            SavePublicacionViewModel publicacionVm = await _publicacionService.Add(vm);

            if (publicacionVm != null && publicacionVm.Id != 0 && vm.FileImagen != null)
            {
                publicacionVm.Imagen = UploadFile(vm.FileImagen, publicacionVm.Id);
                await _publicacionService.Update(publicacionVm, publicacionVm.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> Edit(int Id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToAction("Index");
            }
            SavePublicacionViewModel publicacionVm = await _publicacionService.GetByIdSaveViewModel(Id);
            return RedirectToAction("IndexEdit", publicacionVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", vm);
            }

            SavePublicacionViewModel publicacionVm = await _publicacionService.GetByIdSaveViewModel(vm.Id);
            vm.Imagen = UploadFile(vm.FileImagen, publicacionVm.Id, true, publicacionVm.Imagen);
            await _publicacionService.Update(vm,vm.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _publicacionService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _publicacionService.Delete(id);

            string basePath = $"/Images/Publicaciones/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> Comentar(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveComentarioViewModel comentarioVm = new();
            comentarioVm.IdPublicacion = vm.IdPublicacion;
            comentarioVm.IdUsuario = userViewModel.Id;
            comentarioVm.Detalle = vm.Comentario;

            await _comentarioService.Add(comentarioVm);
            return RedirectToAction("Index");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }
            string basePath = $"/Images/Publicaciones/{id}";
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

            if (isEditMode && imageUrl != null)
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
