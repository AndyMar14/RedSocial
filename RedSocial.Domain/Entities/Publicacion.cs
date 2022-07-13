using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Domain.Entities
{
    public class Publicacion
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Detalle { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        //navigation property

        public User User { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}
