using System.Diagnostics;

using Grpc.Core;

using GrpcLearning.Services;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GrpcLearning
{
	public class GreeterHealthCheck : IHealthCheck
	{
		private readonly GreeterService greeterService;

		public GreeterHealthCheck(GreeterService greeterService)
        {
			this.greeterService = greeterService;
		}
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			Stopwatch sw = Stopwatch.StartNew();
			greeterService.SayHello(new HelloRequest(){ Name = "A"}, context: default );
			if (sw.ElapsedMilliseconds >=50)
			{
				return Task.FromResult(new HealthCheckResult(HealthStatus.Degraded));
			}
			else
			{
				return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy));
			}
			

			throw new NotImplementedException();
		}
	}
}
