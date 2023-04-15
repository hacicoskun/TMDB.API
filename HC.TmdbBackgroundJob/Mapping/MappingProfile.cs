using AutoMapper;
using HC.Shared.Domain.Entities;
using HC.TmdbBackgroundJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.TmdbBackgroundJob.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap(); 
        }
    }
}
