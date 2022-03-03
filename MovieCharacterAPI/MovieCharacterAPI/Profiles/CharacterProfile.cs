using AutoMapper;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(c => c.Movies,
                opt => opt.MapFrom(m => m.Movies
                .Select(m => m.MovieId).ToArray()));
            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterEditDTO, Character>();
        }
    }
}
