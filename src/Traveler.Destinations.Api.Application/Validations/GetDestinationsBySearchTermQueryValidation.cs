using FluentValidation;
using Traveler.Destinations.Api.Application.Queries;

namespace Traveler.Destinations.Api.Application.Validations;

public class GetDestinationsBySearchTermQueryValidation : AbstractValidator<GetDestinationsBySearchTermQuery>
{
    public GetDestinationsBySearchTermQueryValidation()
    {
        ValidateSearchTerm();
    }

    private void ValidateSearchTerm()
    {
        RuleFor(comm => comm.SearchTerm)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um termo de busca")
            .WithErrorCode("88");
    }
}
