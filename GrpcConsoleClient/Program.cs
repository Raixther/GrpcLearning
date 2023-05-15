// See https://aka.ms/new-console-template for more information
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcConsoleClient.Interceptors;
using GrpcLearning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Grpc.HealthCheck;
using Grpc.Health.V1;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services=>{
    services.AddLogging();
    services.AddScoped<ClientLoggingInterceptor>();
}).ConfigureAppConfiguration(conf=>{
    conf.AddJsonFile("appsettings.json");  
}).Build();


IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();

var channel = GrpcChannel.ForAddress(configuration.GetConnectionString("GrpcConnection"));
var invoker = channel.Intercept(host.Services.GetRequiredService<ClientLoggingInterceptor>());  
var client = new Greeter.GreeterClient(invoker);
var result = await client.SayHelloAsync(new HelloRequest(){ Name= "Q"});
Console.WriteLine(result.Message);

var healthClient = new Health.HealthClient(channel);
var healthResponse = await healthClient.CheckAsync(new HealthCheckRequest());
var status = healthResponse.Status.ToString();
Console.WriteLine(status);

host.Run();