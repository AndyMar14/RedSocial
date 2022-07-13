using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Domain.Entities
{
    public class Amigos
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdAmigo { get; set; }
        public int Estado { get; set; }

        public User Amigo { get; set; }
    }
}
