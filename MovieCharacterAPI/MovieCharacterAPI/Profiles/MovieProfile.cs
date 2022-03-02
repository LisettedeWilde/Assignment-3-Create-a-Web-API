using AutoMapper;
using MovieCharacterAPI.Models.DTOs.MovieDTOs;
using MovieCharacterAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(m => m.Characters,
                opt => opt.MapFrom(c => c.Characters
                .Select(c => c.Name).ToArray()))
                .ForMember(m => m.Franchise,
                opt => opt.MapFrom(f => f.Franchise.Name
                .ToString()));
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieEditDTO, Movie>();
        }
    }
}
