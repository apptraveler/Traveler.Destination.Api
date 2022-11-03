using FluentValidation.Results;
using MediatR;

namespace Traveler.Destination.Api.Application.Queries;

public abstract class Query<T> : IRequest<T>
{
    protected ValidationResult ValidationResult { get; set; }

    public ValidationResult GetValidationResult() => ValidationResult;

    public abstract bool IsValid();
}
