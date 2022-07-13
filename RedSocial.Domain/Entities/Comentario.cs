using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Domain.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        //NAVIGATION PROPERTIE

        public User User { get; set; }
        public Publicacion Publicacion { get; set; }
    }
}
