using AutoMapper;
using Shortha.Models;

namespace Shortha.DTO
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Url, PublicUrlResponse>()
                .ForMember(dest => dest.url, o => o.MapFrom(src => src.OriginalUrl));
            CreateMap<Url, UrlCreateRequest>()
                .ForMember(dest => dest.url, opt => opt.MapFrom(src => src.OriginalUrl))
                .ReverseMap()
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.url));
        }
    }

}
