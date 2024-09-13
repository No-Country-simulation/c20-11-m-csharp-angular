using Microsoft.AspNetCore.Diagnostics;
using Tastys.BLL;

namespace Tastys.API.Middlewares;

/// <summary>
/// Controla las excepciones de dominio y genera una respuesta estandarizada.
/// </summary>
public class ErrorHandler : IExceptionHandler
{
    private readonly ILogger<ErrorHandler> _logger;
    private readonly IHostEnvironment _environment;

    public ErrorHandler(ILogger<ErrorHandler> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var handled = await (exception switch
        {
            NotFoundException => HandleNotFoundException(context, (NotFoundException)exception, cancellationToken),
            ValidationException => HandleValidationException(context, (ValidationException)exception, cancellationToken),
            UnauthorizedException => HandleUnauthorizedException(context, (UnauthorizedException)exception, cancellationToken),
            _ => HandleGeneralException(context, exception, cancellationToken)
        });

        await context.Response.CompleteAsync();

        return handled;
    }

    private async Task<bool> HandleNotFoundException(HttpContext context, NotFoundException exception, CancellationToken cancellationToken)
    {
        var errorDto = new ErrorResponseDto()
        {
            Message = _environment.IsProduction()
               ? "No se encontró el recurso solicitado."
               : exception.Message
        };

        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        return true;
    }

    private async Task<bool> HandleValidationException(HttpContext context, ValidationException exception, CancellationToken cancellationToken)
    {
        var errorDto = new ValidationErrorDto()
        {
            Message = _environment.IsProduction()
                ? "El parámetro ingresado es inválido."
                : exception.Message,
            Parameter = exception.Parameter
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        return true;
    }

    private async Task<bool> HandleUnauthorizedException(HttpContext context, UnauthorizedException exception, CancellationToken cancellationToken)
    {
        var errorDto = new ErrorResponseDto()
        {
            Message = _environment.IsProduction()
                ? "No tiene permisos para hacer esto."
                : exception.Message
        }; 

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        return true;
    }

    private async Task<bool> HandleGeneralException(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var errorDto = new ErrorResponseDto() { Message = "Error interno del servidor." };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        _logger.LogError(exception, "Excepción desconocida no controlada.");

        return _environment.IsProduction();
    }
}
