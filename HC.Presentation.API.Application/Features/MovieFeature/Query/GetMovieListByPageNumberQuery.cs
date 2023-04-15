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
    public class GetMovieListByPageNumberQuery : IRequest<List<MovieDTO>>
    {
        public int PageNumber { get; set; }
        public class GetMovieListByPageNumberQueryHandler : IRequestHandler<GetMovieListByPageNumberQuery, List<MovieDTO>>
        {
            private readonly IPostgreDbContext _db;
            private readonly IMapper _mapper;

            public GetMovieListByPageNumberQueryHandler(PostgreDbContext dbContext, IMapper mapper)
            {
                _db = dbContext;
                _mapper = mapper;
            }
            public async Task<List<MovieDTO>> Handle(GetMovieListByPageNumberQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Movies.Where(x => x.page == request.PageNumber).ToListAsync(cancellationToken);

                var dtoList = _mapper.Map<List<MovieDTO>>(data);
                return dtoList;
            }
        }
    }
}
 