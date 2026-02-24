using DnsClient.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComputerStore.DL.Infrastructure.HostedServices
{
    internal class HostedWorker : IHostedService
    {
        private readonly ILogger<HostedWorker> _logger;
        public HostedWorker(ILogger<HostedWorker> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Test: {DateTime.UtcNow}");
                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
