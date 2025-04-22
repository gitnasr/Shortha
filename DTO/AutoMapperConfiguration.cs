using AutoMapper;
using Shortha.Models;

namespace Shortha.DTO
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AppUser, RegisterRequestPayload>().ReverseMap();
            CreateMap<Url, CreatedUrl>().ForMember(dest => dest.Hash, source => source.MapFrom(

                att => att.ShortHash
                )).ReverseMap();
            CreateMap<Url, CreateCustomUrlRequest>()
                .ForMember(src => src.custom, options => options.MapFrom(dest => dest.ShortHash))
                .ForMember(src => src.Url, options => options.MapFrom(dest => dest.OriginalUrl))
                .ReverseMap();

            CreateMap<Visit, UrlVisitsResponse>().ReverseMap();


            CreateMap<Url, PublicUrlResponse>()
                .ForMember(dest => dest.url, o => o.MapFrom(src => src.OriginalUrl));
            CreateMap<Url, UrlCreateRequest>()
                .ForMember(dest => dest.url, opt => opt.MapFrom(src => src.OriginalUrl))
                .ReverseMap()
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.url));
        }
    }

}
