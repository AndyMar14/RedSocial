using AutoMapper;
using RedSocial.Application.ViewModels.Amigos;
using RedSocial.Application.ViewModels.Comentario;
using RedSocial.Application.ViewModels.Publicacion;
using RedSocial.Core.Application.ViewModels.Comentario;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<User, SaveUserViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.FileFoto, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Publicaciones, opt => opt.Ignore());
                //.ForMember(dest => dest.Comentarios, opt => opt.Ignore());

            CreateMap<Publicacion, PublicacionViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.FotoUsuario, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Publicacion, SavePublicacionViewModel>()
                .ForMember(dest => dest.FileImagen, opt => opt.Ignore())
                .ForMember(dest => dest.Comentario, opt => opt.Ignore())
                .ForMember(dest => dest.IdPublicacion, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Comentarios, opt => opt.Ignore());

            CreateMap<Amigos, AmigosViewModel>()
                .ForMember(dest => dest.NombreAmigo, opt => opt.Ignore())
                .ForMember(dest => dest.ApellidoAmigo, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioAmigo, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Amigos, SaveAmigosViewModel>()
                .ForMember(dest => dest.NombreAmigo, opt => opt.Ignore())
                .ForMember(dest => dest.Comentario, opt => opt.Ignore())
                .ForMember(dest => dest.IdPublicacion, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Amigo, opt=> opt.Ignore());

            CreateMap<Comentario, ComentarioViewModel>()
                .ReverseMap();

            CreateMap<Comentario, SaveComentarioViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Publicacion, opt => opt.Ignore());

        }
    }
}
