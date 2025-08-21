using AutoMapper;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Data.Models;

namespace PropertySearch.Business.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Space, SpaceDTO>().ReverseMap();
        }
    }
}
