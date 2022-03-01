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
            CreateMap<Movie, MovieReadDTO>();
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieEditDTO, Movie>();
        }
    }
}
