﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static Grpc.Core.Interceptors.Interceptor;

namespace GrpcConsoleClient.Interceptors
{
    internal class ClientLoggingInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ClientLoggingInterceptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ClientLoggingInterceptor>();
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
