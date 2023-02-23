namespace Lonsid.Fusion.Infrastructure.Middleware;

public class ValidatorMiddleware<TEvent> : IEventMiddleware<TEvent>
    where TEvent : notnull, IEvent
{
    private readonly ILogger<ValidatorMiddleware<TEvent>> _logger;
    private readonly IEnumerable<IValidator<TEvent>> _validators;

    public ValidatorMiddleware(IEnumerable<IValidator<TEvent>> validators, ILogger<ValidatorMiddleware<TEvent>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public bool SupportRecursive => true;

    public async Task HandleAsync(TEvent action, EventHandlerDelegate next)
    {
        var typeName = action.GetType().FullName;

        _logger.LogInformation("----- Validating command {CommandType}", typeName);

        var failures = _validators
            .Select(v => v.Validate(action))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.LogInformation("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}, ErrorMessage: {ErrorMessage}", typeName, action, failures, string.Join(",", failures.Select(p => p.ErrorMessage)));

            throw new ValidationException("Validation exception", failures);
        }

        await next();
    }
}
