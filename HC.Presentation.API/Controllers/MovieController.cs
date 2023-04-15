using FluentValidation;
using FluentValidation.Results;
using HC.Api.Identity.Identity;
using HC.Presentation.API.Application.DTOs;
using HC.Presentation.API.Application.Features.MovieFeature.Command;
using HC.Presentation.API.Application.Features.MovieFeature.Query;
using HC.Presentation.API.Models;
using HC.Presentation.API.Validation.Movie;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    }
}
