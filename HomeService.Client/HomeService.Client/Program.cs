using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using HomeService.Server;

namespace HomeService.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions {HttpHandler = httpHandler});
            
            var greeterClient = new Greeter.GreeterClient(channel);
            var greeterReply = await greeterClient.SayHelloAsync(
                new HelloRequest {Name = "GreeterClient"});
            Console.WriteLine($"Greeting: {greeterReply.Message}");

            var indicatorClient = new Indicator.IndicatorClient(channel);
            var indicatorReply = await indicatorClient.IndicateAsync(
                new IndicateRequest {Color = 1, Duration = 200});
            Console.WriteLine($"Indicator: {indicatorReply.Indicated}");
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}