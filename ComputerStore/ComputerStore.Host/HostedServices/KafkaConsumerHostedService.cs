using Confluent.Kafka;

namespace ComputerStore.Host.HostedServices
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IConsumer<int, string> _consumer;
        private readonly string _topic;
        private readonly ILogger<KafkaConsumerHostedService> _logger;

        public KafkaConsumerHostedService(IConfiguration config, ILogger<KafkaConsumerHostedService> logger)
        {
            _topic = "pu-chat";
            _logger = logger;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "kafka-210718-0.cloudclusters.net:10020",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "puchat",
                SaslPassword = "1234567q",
                EnableSslCertificateVerification = false,
                GroupId = $"KafkaChat{Guid.NewGuid}",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            _consumer = new ConsumerBuilder<int, string>(consumerConfig).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);

            return Task.Run(() =>
            {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);
                        Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                    }
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    _consumer.Close();
                }
            }, stoppingToken);
        }
    }
}
