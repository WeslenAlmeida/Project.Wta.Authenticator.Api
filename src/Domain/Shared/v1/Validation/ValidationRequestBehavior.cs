using CrossCutting.Exception.CustomExceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Shared.v1.Validation
{
     public class ValidationRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TResponse> _logger;

        public ValidationRequestBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TResponse> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start Validation {DateTime.Now.ToLongTimeString()}");
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,

                (propertyName, errorMessages) => new
                {
                    PropertyName = propertyName,
                    ErrorMessage = errorMessages.Distinct().ToArray()
                }).ToList();

                if (failures.Count != 0)
                {
                    _logger.LogInformation($"Error Validation {DateTime.Now.ToLongTimeString()}");
                    throw new BehaviorBadRequestException(failures);
                }
            }
            _logger.LogInformation($"End Validation {DateTime.Now.ToLongTimeString()}");
            return await next();
        }
    }
}