using RedSocial.Core.Application.ViewModels.Comentario;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModels.Publicacion
{
    public class PublicacionViewModel
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string FotoUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public UserViewModel User { get; set; }
        public ICollection<ComentarioViewModel> Comentarios { get; set; }
    }
}
