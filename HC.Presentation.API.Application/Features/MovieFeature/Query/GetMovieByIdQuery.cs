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
    public class GetMovieByIdQuery : IRequest<MovieAndCommentsDTO>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieAndCommentsDTO>
        {
            private readonly IPostgreDbContext _db;
            private readonly IMapper _mapper;

            public GetMovieByIdQueryHandler(PostgreDbContext dbContext, IMapper mapper)
            {
                _db = dbContext;
                _mapper = mapper;
            }
            public async Task<MovieAndCommentsDTO> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Movies.FirstOrDefaultAsync(x => x.movie_id == request.Id);
                if (data is not null)
                {
                    MovieAndCommentsDTO item = new MovieAndCommentsDTO();
                    item.movie = _mapper.Map<MovieDTO>(data);
                    item.note_and_scores = await _db.MovieComments.Where(x => x.movie_id == data.movie_id && x.user_id == request.UserId).Select(s => new NoteAndScores
                    {
                        note = s.note,
                        score = s.score,
                    }).ToListAsync();
                    item.average_score = item.note_and_scores.Any() ? item.note_and_scores.Average(s => s.score) : 0;

                    return item; 
                }
                else
                {
                    return new MovieAndCommentsDTO
                    {
                        response = "Film Bulunamadı.",
                        response_code = "404"
                    };
                }
            }
        }
    }
}
 