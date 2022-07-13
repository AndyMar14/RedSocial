using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModels.Amigos
{
    public class SaveAmigosViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Debes escribir el nombre de tu amigo")]
        public string NombreAmigo { get; set; }
        public int IdUsuario { get; set; }
        public int IdAmigo { get; set; }
        public int Estado { get; set; }
        public string Comentario { get; set; }
        public int IdPublicacion { get; set; }
    }
}
