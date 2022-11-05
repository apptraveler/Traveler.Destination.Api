using FluentValidation.Results;
using MediatR;

namespace Traveler.Destinations.Api.Application.Queries;

public abstract class Query<T> : IRequest<T>
{
    protected ValidationResult ValidationResult { get; set; }

    public ValidationResult GetValidationResult() => ValidationResult;

    public abstract bool IsValid();
}
