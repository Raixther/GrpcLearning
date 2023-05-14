using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcConsoleClient.Interceptors
{
    internal class ClientLoggingInterceptor : Interceptor
    {
        private readonly ILogger<ClientLoggingInterceptor> _logger;

        public ClientLoggingInterceptor(ILogger<ClientLoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"Starting call. Type: {context.Method.Type}. " +
                $"Method: {context.Method.Name}.");
            return continuation(request, context);
        }
    }
}
