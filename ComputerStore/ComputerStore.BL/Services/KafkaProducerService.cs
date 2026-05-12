using ComputerStore.BL.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace ComputerStore.BL.Services
{
    internal class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<int, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration config)
        {
            _topic = "pu-chat";

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "kafka-210718-0.cloudclusters.net:10020",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "puchat",
                SaslPassword = "1234567q",
                EnableSslCertificateVerification = false 
            };

            _producer = new ProducerBuilder<int, string>(producerConfig).Build();
        }

        public async Task ProduceSaleMessageAsync(int key, string message)
        {
            try
            {
                var deliveryReport = await _producer.ProduceAsync(_topic, new Message<int, string> { Key = key, Value = message });
                Console.WriteLine($"Message delivered to {deliveryReport.TopicPartitionOffset}");
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
            }
        }
    }
}
