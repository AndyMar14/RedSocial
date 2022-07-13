using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModels.Publicacion
{
    public class SavePublicacionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Escribe un detalle")]
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int IdPublicacion { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile FileImagen { get; set; }
    }
}
