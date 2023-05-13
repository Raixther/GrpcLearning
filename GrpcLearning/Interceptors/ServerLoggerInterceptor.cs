using Grpc.Core.Interceptors;
using Grpc.Core;

namespace GrpcLearning.Interceptors
{
    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"Starting receiving call. Type: {MethodType.Unary}. " +
                $"Method: {context.Method}.");
            try
            {
                _logger.LogInformation($"Status {context.Status.StatusCode}");
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Status {context.Status.StatusCode}");
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }
}
