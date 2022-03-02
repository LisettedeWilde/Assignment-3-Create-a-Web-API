using AutoMapper;
using MovieCharacterAPI.Models.DTOs.CharacterDTOs;
using MovieCharacterAPI.Models.DTOs.FranchiseDTOs;
using MovieCharacterAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            //CreateMap<Franchise, FranchiseReadDTO>(); // TODO: update to include movies and characters
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(f => f.Movies,
                opt => opt.MapFrom(m => m.Movies
                .Select(m => m.Title).ToArray()));
            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseEditDTO, Franchise>();
        }
    }
}
