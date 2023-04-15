using FluentValidation;
using HC.Presentation.API.Models;
using Microsoft.Extensions.Localization;

namespace HC.Presentation.API.Validation.Movie
{
   
    public class AddMovieCommentRequestValidator : AbstractValidator<AddMovieCommentRequestModel>
    {
        public AddMovieCommentRequestValidator()
        {
             
            RuleFor(c => c.Note).NotEmpty().WithMessage("Not alanı boş bırakılamaz.");
            RuleFor(x => x.Score).NotNull().WithMessage("Puan alanı boş bırakılamaz.").InclusiveBetween(1, 10).WithMessage("Puan değeri 1-10 arası olmalıdır.");

        }
    }
}
