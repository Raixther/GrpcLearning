// See https://aka.ms/new-console-template for more information
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcConsoleClient.Interceptors;
using GrpcLearning;
using Microsoft.Extensions.Logging;

var channel = GrpcChannel.ForAddress("https://localhost:7214");
var invoker = channel.Intercept(new ClientLoggingInterceptor(new LoggerFactory()));  
var client = new Greeter.GreeterClient(invoker);


var result = await client.SayHelloAsync(new HelloRequest(){ Name= "Q"});
Console.WriteLine(result.Message);      
