using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace ComputerStore.DL.Infrastructure.HostedServices
{
    internal class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;
        public BackgroundWorker(ILogger<BackgroundWorker> logger)
        {
            _logger = logger;
        }
        
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Test: {DateTime.UtcNow}");
                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
