using GrpcLearning;
using GrpcLearning.Interceptors;
using GrpcLearning.Services;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc().AddServiceOptions<GreeterService>(cfg=>{
cfg.Interceptors.Add<ServerLoggerInterceptor>();
});
builder.Services.AddScoped<GreeterService>();

//builder.Services.AddGrpcHealthChecks(cfg =>
//{
//	cfg.Services.MapService("greet.Greeter", s => s.Tags.Contains("greeter"));
//}).AddCheck<GreeterHealthCheck>("greeterCheck");

builder.Services.AddHealthChecks().AddCheck<GreeterHealthCheck>("greeterCheck") ;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<GreeterService>();
//app.MapGrpcHealthChecksService().WithTags("greeter");
app.MapHealthChecks("/health") ;
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
