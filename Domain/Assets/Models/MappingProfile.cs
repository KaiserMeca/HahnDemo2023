using AutoMapper;

namespace Domain.Assets.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetDTO>();
            CreateMap<AssetDTO, Asset>();
            //.ForMember(dest => dest.Id, opt => opt.Ignore());
            //CreateMap<AssetDTO, Asset>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
