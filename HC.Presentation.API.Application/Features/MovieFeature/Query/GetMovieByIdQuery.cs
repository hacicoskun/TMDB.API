using AutoMapper;
using HC.Presentation.API.Application.DTOs;
using HC.Shared.Application.Interfaces;
using HC.Shared.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Presentation.API.Application.Features.MovieFeature.Query
{
    public class GetMovieByIdQuery : IRequest<List<MovieDTO>>
    {
        public int Id { get; set; }
        public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, List<MovieDTO>>
        {
            private readonly IPostgreDbContext _db;
            private readonly IMapper _mapper;

            public GetMovieByIdQueryHandler(PostgreDbContext dbContext, IMapper mapper)
            {
                _db = dbContext;
                _mapper = mapper;
            }
            public async Task<List<MovieDTO>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Movies.FirstOrDefaultAsync(x => x.movie_id == request.Id);

                var dtoList = _mapper.Map<List<MovieDTO>>(data);
                return dtoList;
            }
        }
    }
}
 