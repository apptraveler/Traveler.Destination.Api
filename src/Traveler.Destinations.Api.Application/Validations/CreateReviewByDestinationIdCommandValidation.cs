using FluentValidation;
using Traveler.Destinations.Api.Application.Commands;

namespace Traveler.Destinations.Api.Application.Validations;

public class CreateReviewByDestinationIdCommandValidation : AbstractValidator<CreateReviewByDestinationIdCommand>
{
    public CreateReviewByDestinationIdCommandValidation()
    {
        ValidateRate();
        ValidateMessage();
        ValidateDestinationId();
    }

    private void ValidateRate()
    {
        RuleFor(c => c.Rate)
            .Must(rate => rate is > 0 and <= 5)
            .WithErrorCode("88")
            .WithMessage("Informe sua nota de review em valores entre 0 e 5");
    }

    private void ValidateMessage()
    {
        RuleFor(c => c.Message)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe uma mensagem de review");
    }

    private void ValidateDestinationId()
    {
        RuleFor(c => c.DestinationId)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("88")
            .WithMessage("Informe o id do destino");
    }
}
