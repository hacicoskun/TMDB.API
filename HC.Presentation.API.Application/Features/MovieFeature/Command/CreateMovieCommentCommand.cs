using AutoMapper;
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
   
    public class CreateMovieCommentCommand : IRequest<string>
    {
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public class CreateMovieCommentCommandHandler : IRequestHandler<CreateMovieCommentCommand, string>
        {
            private readonly IPostgreDbContext _db; 

            public CreateMovieCommentCommandHandler(PostgreDbContext dbContext, IMapper mapper)
            {
                _db = dbContext; 
            }
            public async Task<string> Handle(CreateMovieCommentCommand request, CancellationToken cancellationToken)
            {
                MovieComments movieComment = new MovieComments
                {
                    movie_id = request.MovieId,
                    note = request.Note,
                    score = request.Score,
                    user_id= request.UserId
                };
               
                await _db.MovieComments.AddAsync(movieComment,cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return "";
            }
        }
    }
}
