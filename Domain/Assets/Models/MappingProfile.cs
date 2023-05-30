using AutoMapper;

namespace Domain.Assets.Models
{
    /// <summary>
    /// Represents the mapping profile for asset models
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the MappingProfile class
        /// </summary>
        public MappingProfile()
        {
            /// <summary>
            /// Creates a map from Asset to AssetDTO
            /// </summary>
            CreateMap<Asset, AssetDTO>();

            /// <summary>
            /// Creates a map from AssetDTO to Asset
            /// </summary>
            CreateMap<AssetDTO, Asset>();
        }
    }
}
