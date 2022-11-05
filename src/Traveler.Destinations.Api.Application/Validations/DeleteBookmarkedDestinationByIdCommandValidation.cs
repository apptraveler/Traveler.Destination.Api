using FluentValidation;
using Traveler.Destinations.Api.Application.Commands;

namespace Traveler.Destinations.Api.Application.Validations;

public class DeleteBookmarkedDestinationByIdCommandValidation : AbstractValidator<DeleteBookmarkedDestinationByIdCommand>
{
    public DeleteBookmarkedDestinationByIdCommandValidation()
    {
        ValidateUserId();
        ValidateDestinationId();
    }

    private void ValidateUserId()
    {
        RuleFor(comm => comm.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O usuário não possui UserId")
            .WithErrorCode("88");
    }

    private void ValidateDestinationId()
    {
        RuleFor(comm => comm.DestinationId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe o id do destino")
            .WithErrorCode("88");
    }
}
