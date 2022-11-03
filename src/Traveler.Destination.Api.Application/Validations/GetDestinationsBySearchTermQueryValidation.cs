using FluentValidation;
using Traveler.Destination.Api.Application.Queries;

namespace Traveler.Destination.Api.Application.Validations;

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
