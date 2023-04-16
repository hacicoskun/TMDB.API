using FluentValidation.Results;
using HC.Presentation.API.Application.DTOs;
using HC.Presentation.API.Application.Features.MovieFeature.Command;
using HC.Presentation.API.Application.Features.MovieFeature.Query;
using HC.Presentation.API.Models;
using HC.Presentation.API.Validation.Movie;
using HC.Shared;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HC.Presentation.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MovieController(IMediator mediator, ISendEndpointProvider sendEndpointProvider)
        {
            _mediator = mediator;
            this._sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<List<MovieDTO>> MovieList(GetMovieListByPageNumberQuery request)
        {
            var data = await _mediator.Send(request);
            return data;
        }

        [HttpPost]
        [Route("AddMovieComment")]
        public async Task<MovieCommentDTO> AddMovieComment(AddMovieCommentRequestModel command)
        {

            AddMovieCommentRequestValidator validator = new AddMovieCommentRequestValidator();

            ValidationResult v_result = validator.Validate(new AddMovieCommentRequestModel { Note = command.Note, Score = command.Score });
            if (!v_result.IsValid)
            {
                string errors = "";
                foreach (ValidationFailure failer in v_result.Errors)
                {
                    errors += failer.ErrorMessage;
                }
                ModelState.AddModelError("Input.Email", errors);
                return new MovieCommentDTO
                {
                    response = errors,
                    response_code = 500
                };
            }


            var data = await _mediator.Send(new CreateMovieCommentCommand { MovieId = command.MovieId, Note = command.Note, Score = command.Score, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });

            return data;
        }


        [HttpPost]
        [Route("FindMovie")]
        public async Task<MovieAndCommentsDTO> GetMovieById(GetMovieByIdRequestModel query)
        {
            var data = await _mediator.Send(new GetMovieByIdQuery { Id = query.Id, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });

            return data;
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<ResponseModel> SuggestMoviesByEmail(SuggestMoviesByEmailRequestModel request)
        {
            var data = await _mediator.Send(new GetMovieByIdQuery { Id = request.movie_id, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });

            if (data is not null)
            {
                var send = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-email-request-by-rabbitmq"));
                var item = new CreateRecommendMovieWithEmail { Email = request.email ,Body= "HC.RabbitMQListener Test  Lorem Ipsum is simply dummy text of the printing and typesetting industry.",Title= "RabbitMQ-Masstransit HC.RabbitMQListener" };
                await send.Send<CreateRecommendMovieWithEmail>(item);
                return new ResponseModel { response = data.movie.title + " isimli film önerisi " + request.email + " adresine gönderildi." ,response_code= "200" };
            }
            return new ResponseModel { response = "", response_code = "500" };

        }
    }
}
