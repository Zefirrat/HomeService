using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace HomeService.Server
{
    public class IndicatorService : Indicator.IndicatorBase
    {
        public IndicatorService(ILogger<IndicatorService> logger)
        {
            _logger = logger;
        }

        private ILogger _logger { get; }

        public override Task<IndicateReply> Indicate(IndicateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new IndicateReply {Indicated = true});
        }
    }
}