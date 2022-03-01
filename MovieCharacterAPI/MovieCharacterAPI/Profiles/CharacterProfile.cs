using AutoMapper;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>();
            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterEditDTO, Character>();
        }
    }
}
