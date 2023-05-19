using AutoMapper;

namespace Domain.Security.Agregate
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
