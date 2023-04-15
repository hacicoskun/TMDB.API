using AutoMapper;
using HC.Presentation.API.Application.DTOs;
using HC.Shared.Domain.Entities;

namespace HC.Presentation.API.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap(); 
            CreateMap<MovieComments, MovieCommentDTO>().ReverseMap(); 
        }
    }
}
