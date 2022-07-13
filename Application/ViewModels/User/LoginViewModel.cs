using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debes colocar un nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes colocar una clave")]
        public string Password { get; set; }
    }
}
