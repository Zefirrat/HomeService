using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace Indicator.Client
{
    public class IndicatorClient : IIndicatorClient
    {
        private SemaphoreSlim _semaphoreReadyToSend = new SemaphoreSlim(1, 1);

        private async Task Authorize(PrimS.Telnet.Client telnet)
        {
            var tmpString = string.Empty;

            var credentials = new {Login = "micro", Password = "python"};
            
            tmpString = await WaitMessage(telnet);
            await telnet.WriteLine(credentials.Login);
            Thread.Sleep(500);
            // tmpString = await WaitMessage(telnet);
            await telnet.WriteLine(credentials.Password);
            tmpString = await WaitMessage(telnet);
        }

        private async Task<string> WaitMessage(PrimS.Telnet.Client telnet)
        {
            var receive = string.Empty;
            while (!receive.Any())
            {
                receive = await telnet.ReadAsync();
                Thread.Sleep(500);
            }

            return receive;
        }
        
        private async Task<bool> SendCommand(string command)
        {
            bool result = false;
            try
            {
                var serverSettings = new {Server = "192.168.31.75", Port = 23};

                using (var telnet = new PrimS.Telnet.Client(serverSettings.Server, serverSettings.Port, new CancellationToken()))
                {
                    telnet.IsConnected.Should().Be(true);
                    await Authorize(telnet);
                    
                    await telnet.Write(command);
                    string response = await telnet.TerminatedReadAsync(">", TimeSpan.FromMilliseconds(10000));
                }

                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        public async Task<bool> IndicateAsync(int color)
        {
            return await SendCommand($"leds.setAllRGB({color})");
        }
    }
}