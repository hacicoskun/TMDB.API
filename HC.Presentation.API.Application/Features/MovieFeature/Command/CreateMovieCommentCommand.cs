using AutoMapper;
using HC.Presentation.API.Application.DTOs;
using HC.Shared.Application.Interfaces;
using HC.Shared.Domain.Entities;
using HC.Shared.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Presentation.API.Application.Features.MovieFeature.Command
{
   
    public class CreateMovieCommentCommand : IRequest<MovieCommentDTO>
    {
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public class CreateMovieCommentCommandHandler : IRequestHandler<CreateMovieCommentCommand, MovieCommentDTO>
        {
            private readonly IPostgreDbContext _db; 
            private readonly IMapper _mapper; 

            public CreateMovieCommentCommandHandler(PostgreDbContext dbContext, IMapper mapper)
            {
                _db = dbContext; 
                _mapper = mapper;
            }
            public async Task<MovieCommentDTO> Handle(CreateMovieCommentCommand command, CancellationToken cancellationToken)
            {
                MovieComments movieComment = new MovieComments
                {
                    movie_id = command.MovieId,
                    note = command.Note,
                    score = command.Score,
                    user_id= command.UserId
                };
               
                await _db.MovieComments.AddAsync(movieComment,cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                var data = _mapper.Map<MovieCommentDTO>(movieComment);
                data.response = "Yorum ekleme işlemi başarı ile tamamlandı";
                data.response_code = 200;

                return data;
            }
        }
    }
}
