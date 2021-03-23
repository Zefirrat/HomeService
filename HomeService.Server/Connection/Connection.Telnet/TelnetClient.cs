using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Connection.Telnet
{
    public class TelnetClient:ITelnetClient
    {
        private TcpClient _client;

        private async Task Authorize(TcpClient telnet)
             {
               
             }
        public async Task Connect()
        {
            var serverSettings = new {Server = "192.168.31.75", Port = 23};
            _client = new TcpClient();
            await _client.ConnectAsync(serverSettings.Server, serverSettings.Port);
            await Authorize(_client);
        }

        public async Task Send(string message)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> Read()
        {
            throw new System.NotImplementedException();
        }
    }
}