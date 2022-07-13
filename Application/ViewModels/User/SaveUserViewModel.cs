using Microsoft.AspNetCore.Http;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        [Required(ErrorMessage = "Debes colocar un nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes colocar un apellido")]
        public string Apellido { get; set; }
        public string Foto { get; set; }
        public string Correo { get; set; }
        [Required(ErrorMessage = "Debes colocar un telefono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debes colocar un usuario")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Debes colocar una clave")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes repetir la clave")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las claves deben ser iguales")]
        public string ConfirmPassword { get; set; }


        //NAVIGATIONS PROPERTYES
        [DataType(DataType.Upload)]
        public IFormFile FileFoto { get; set; }
    }
}
