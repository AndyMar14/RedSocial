using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Application.ViewModels.Comentario;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.RedSocial.Middleware;
using System.Threading.Tasks;

namespace WebApp.RedSocial.Controllers
{
    public class AmigosController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicacionService _publicacionService;
        private readonly IAmigosService _amigosService;
        private readonly IComentarioService _comentarioService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public AmigosController(ValidateUserSession validateUserSession, IPublicacionService publicacionService, IAmigosService amigosService, IUserService userService, IHttpContextAccessor httpContextAccessor, IComentarioService comentarioService)
        {
            _validateUserSession = validateUserSession;
            _publicacionService = publicacionService;
            _amigosService = amigosService;
            _userService = userService;
            _comentarioService = comentarioService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            ;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            
            ViewBag.Amigos = await _amigosService.GetAllAmigosWithIncludes();
            var listaAmigos = await _amigosService.GetAllAmigosId();
            ViewBag.Publicaciones = await _publicacionService.GetAllFriendsViewModel(listaAmigos);

            if (TempData.ContainsKey("UserExist"))
            {
                if ((bool)TempData["UserExist"] == false)
                {
                    ModelState.AddModelError("userValidation", "El usuario no Existe");
                }
            }
            if (TempData.ContainsKey("RelationExist"))
            {
                if ((bool)TempData["RelationExist"] == true)
                {
                    ModelState.AddModelError("userValidation", "Ya son amigos");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveAmigosViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            vm.IdUsuario = userViewModel.Id;
            var user = await _amigosService.GetUserName(vm);


            if (!ModelState.IsValid || user == null)
            {
                TempData["UserExist"] = false;
                return RedirectToAction("Index");
            }

            var amistad = await _amigosService.GetRelationship(userViewModel.Id, user.Id);

            if (amistad == false)
            {
                SaveAmigosViewModel amigo = new();
                amigo.IdAmigo = user.Id;
                amigo.IdUsuario = userViewModel.Id;

                await _amigosService.Add(amigo);

                SaveAmigosViewModel amigoBack = new();
                amigoBack.IdAmigo = userViewModel.Id;
                amigoBack.IdUsuario = user.Id;

                await _amigosService.Add(amigoBack);
            }
            else
            {
                TempData["RelationExist"] = true;
            }
            return RedirectToAction("Index");

            //return RedirectToRoute(new { controller = "Amigos", action = "Index" });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var amistad = await _amigosService.GetRelationshipId(userViewModel.Id, Id);
            await _amigosService.Delete(amistad.Id);
            var amistadBack = await _amigosService.GetRelationshipId(Id, userViewModel.Id);
            await _amigosService.Delete(amistadBack.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Comentar(SaveAmigosViewModel vm)
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
    }
}
