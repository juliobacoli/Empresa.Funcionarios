using Serilog;
using System.Net;
using System.Text.Json;

namespace Empresa.Funcionarios.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro não tratado capturado pelo middleware global.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Ocorreu um erro interno. Tente novamente mais tarde.",
            Error = exception.Message
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(jsonResponse);
    }
}
