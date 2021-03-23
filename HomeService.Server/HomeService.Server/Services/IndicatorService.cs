using System.Threading.Tasks;
using Grpc.Core;
using Indicator.Client;
using Microsoft.Extensions.Logging;

namespace HomeService.Server
{
    public class IndicatorService : Indicator.IndicatorBase
    {
        public IndicatorService(ILogger<IndicatorService> logger, IIndicatorClient indicatorClient)
        {
            _logger = logger;
            _indicatorClient = indicatorClient;
        }

        private ILogger _logger { get; }
        private IIndicatorClient _indicatorClient { get; }

        public override async Task<IndicateReply> Indicate(IndicateRequest request, ServerCallContext context)
        {

            var executionResult = await _indicatorClient.IndicateAsync(request.Color);
            return new IndicateReply {Indicated = executionResult};
        }
    }
}