using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Domain.Entities
{
    public class User
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

        //navigation property

        public ICollection<Publicacion> Publicaciones { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Amigos> Amigos { get; set; }
    }
}
