using AutoMapper;
using EventGo.Application.Dtos;
using EventGo.Domain;
using EventGo.Domain.Identity;

namespace EventGo.Application.Helpers
{
    public class EventGoProfile : Profile
    {
        public EventGoProfile()
        {
            CreateMap<Evento, EventoDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
            CreateMap<Organizador, OrganizadorDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

        }
    }
}