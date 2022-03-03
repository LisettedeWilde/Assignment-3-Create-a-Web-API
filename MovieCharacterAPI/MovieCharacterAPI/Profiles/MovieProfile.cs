using AutoMapper;
using MovieCharacterAPI.Models.DTOs.MovieDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(m => m.Characters,
                opt => opt.MapFrom(c => c.Characters
                .Select(c => c.CharacterId).ToArray()))
                .ForMember(m => m.Franchise,
                opt => opt.MapFrom(f => f.Franchise.FranchiseId
                ));
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieEditDTO, Movie>();
        }
    }
}
