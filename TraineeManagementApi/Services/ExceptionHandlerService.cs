using Microsoft.AspNetCore.Diagnostics;

namespace TraineeManagementApi.Services;

public class ExceptionHandlerService : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerService> logger;
    public ExceptionHandlerService(ILogger<ExceptionHandlerService> logger)
    {
        this.logger = logger;
    }
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exceptionMessage, DateTime.UtcNow);
        return ValueTask.FromResult(false);
    }
}