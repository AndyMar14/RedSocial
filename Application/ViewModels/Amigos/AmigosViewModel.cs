using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModels.Amigos
{
    public class AmigosViewModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreAmigo { get; set; }
        public string ApellidoAmigo { get; set; }
        public string UsuarioAmigo { get; set; }
        public int IdAmigo { get; set; }
        public int Estado { get; set; }

        public UserViewModel Amigo { get; set; }
    }
}
