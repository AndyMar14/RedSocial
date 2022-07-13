using RedSocial.Application.ViewModels.Comentario;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comentario;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IComentarioService : IGenericService<SaveComentarioViewModel, ComentarioViewModel, Comentario>
    {
    }
}
