using System.Diagnostics;
using MediatR;

public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        Debug.WriteLine($"[LOG] {requestName} isteği başladı.");
        var response = await next();
        Debug.WriteLine($"[LOG] {requestName} isteği bitti.");
        return response;
    }
}
