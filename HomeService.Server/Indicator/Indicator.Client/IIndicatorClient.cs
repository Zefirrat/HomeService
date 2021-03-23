using System.Threading.Tasks;

namespace Indicator.Client
{
    public interface IIndicatorClient
    {
        Task<bool> IndicateAsync(int color);
    }

}