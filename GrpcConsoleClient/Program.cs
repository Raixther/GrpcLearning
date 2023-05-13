// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcLearning;


var channel = GrpcChannel.ForAddress("https://localhost:7214");
var client = new Greeter.GreeterClient(channel);
var result = await client.SayHelloAsync(new HelloRequest(){ Name= "Q"});

Console.WriteLine(result.Message);      
