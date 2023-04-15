using HC.Presentation.API.Application.DTOs;
using HC.Presentation.API.Application.Features.MovieFeature.Command;
using HC.Presentation.API.Application.Features.MovieFeature.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HC.Presentation.API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("GetList")]
        public async Task<List<MovieDTO>> MovieList(GetMovieListByPageNumberQuery request)
        {
            var data = await _mediator.Send(request);
            return data;
        }
        [HttpPost]
        [Route("GetById")]
        public async Task<string> GetById(CreateMovieCommentCommand command)
        {
            var data = await _mediator.Send(command);
            return data;
        }
    }
}
