using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.Core.Application.ViewModels.Comentario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Foto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public PublicacionViewModel Publicaciones { get; set; }
        public ComentarioViewModel Comentarios { get; set; }
    }
}
