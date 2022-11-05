using FluentValidation;
using Traveler.Destinations.Api.Application.Queries;

namespace Traveler.Destinations.Api.Application.Validations;

public class GetRecommendedDestinationsByUserIdQueryValidation : AbstractValidator<GetRecommendedDestinationsByUserIdQuery>
{
    public GetRecommendedDestinationsByUserIdQueryValidation()
    {
        ValidateUserId();
        ValidateDestinationPreference();
        ValidateAverageSpendPreference();
    }

    private void ValidateUserId()
    {
        RuleFor(comm => comm.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe o UserId");
    }

    private void ValidateDestinationPreference()
    {
        RuleFor(comm => comm.LocationTagsIdsPreference)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe as preferências de destino");
    }

    private void ValidateAverageSpendPreference()
    {
        RuleFor(comm => comm.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe a taxa média que gostaria de investir");
    }
}
