using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;

namespace RedSocial.RedSocial.Middleware
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (userViewModel == null)
            {
                return false;
            }

            return true;
        }

    }
}
