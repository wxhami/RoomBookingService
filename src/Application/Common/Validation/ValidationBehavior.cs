using FluentValidation;
using MediatR;

namespace Application.Common.Validation;

internal record ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? Validator = default)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (Validator != null)
        {
            await Validator.ValidateAndThrowAsync(request, cancellationToken);
        }

        var response = await next();
        return response;
    }
}