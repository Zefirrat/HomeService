using System.Threading.Tasks;

namespace Connection.Telnet
{
    public interface ITelnetClient
    {
        Task Connect();
        Task Send(string message);
        Task<string> Read();
    }
}