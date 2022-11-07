using AutoMapper;

namespace Beca.PlaylistInfo.API.Profiles
{
    public class CancionProfile : Profile
    {

        public CancionProfile()
        {
            CreateMap<Entities.Cancion , Models.CancionDto>();
            CreateMap<Models.CancionCreationDto , Entities.Cancion>();
            CreateMap<Models.CancionUpdateDto, Entities.Cancion>();
            CreateMap<Entities.Cancion, Models.CancionUpdateDto>();
        }
        
    }
}
