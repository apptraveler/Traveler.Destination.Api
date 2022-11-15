using FluentValidation;
using Traveler.Destinations.Api.Application.Commands;

namespace Traveler.Destinations.Api.Application.Validations;

public class DeleteReviewByIdCommandValidation : AbstractValidator<DeleteReviewByIdCommand>
{
    public DeleteReviewByIdCommandValidation()
    {
        ValidateReviewId();
    }

    private void ValidateReviewId()
    {
        RuleFor(c => c.ReviewId)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe o id da review para ser excluída");
    }
}
