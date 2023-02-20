using AutoMapper;
using HearthenaServer.DTO;
using HearthenaServer.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace HearthenaServer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            base.CreateMap<Hero, HeroDTO>();
            base.CreateMap<Hero, HeroDTO>();

            HeroDTOMap();
        }

        public void HeroDTOMap()
        {
        }
    }
}
