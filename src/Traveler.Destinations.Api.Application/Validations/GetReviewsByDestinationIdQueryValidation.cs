using FluentValidation;
using Traveler.Destinations.Api.Application.Queries;

namespace Traveler.Destinations.Api.Application.Validations;

public class GetReviewsByDestinationIdQueryValidation : AbstractValidator<GetReviewsByDestinationIdQuery>
{
    public GetReviewsByDestinationIdQueryValidation()
    {
        ValidateDestinationId();
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
