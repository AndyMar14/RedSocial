using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Comentario
{
    public class ComentarioViewModel
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        //NAVIGATION PROPERTIE
        public UserViewModel User { get; set; }
        public PublicacionViewModel Publicacion { get; set; }
    }
}
