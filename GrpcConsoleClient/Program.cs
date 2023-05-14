// See https://aka.ms/new-console-template for more information
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcConsoleClient.Interceptors;
using GrpcLearning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services=>{
    services.AddLogging();
    services.AddScoped<ClientLoggingInterceptor>();
}).ConfigureAppConfiguration(conf=>{
    conf.AddJsonFile("appsettings.json");
}).Build();


var channel = GrpcChannel.ForAddress("https://localhost:7214");
var invoker = channel.Intercept(host.Services.GetRequiredService<ClientLoggingInterceptor>());  
var client = new Greeter.GreeterClient(invoker);
var result = await client.SayHelloAsync(new HelloRequest(){ Name= "Q"});
Console.WriteLine(result.Message);
host.Run();