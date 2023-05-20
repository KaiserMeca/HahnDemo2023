using AutoMapper;

namespace Domain.Assets.Aggregates
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Asset, AssetDTO>();
            CreateMap<AssetDTO, Asset>();
        }
    }
}
