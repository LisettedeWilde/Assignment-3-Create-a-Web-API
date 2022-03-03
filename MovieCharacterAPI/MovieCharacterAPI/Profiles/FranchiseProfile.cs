using AutoMapper;
using MovieCharacterAPI.Models.DTOs.FranchiseDTOs;
using MovieCharacterAPI.Models.Domain;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(f => f.Movies,
                opt => opt.MapFrom(m => m.Movies
                .Select(m => m.MovieId).ToArray()));
            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseEditDTO, Franchise>();
        }
    }
}
