using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModels.Comentario
{
    public class SaveComentarioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debes colocar un Comentario")]
        public string Detalle { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
