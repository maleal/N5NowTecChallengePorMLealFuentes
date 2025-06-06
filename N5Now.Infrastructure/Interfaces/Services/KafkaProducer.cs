using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using N5Now.Core.DTOs;
using N5Now.Core.Interfaces.Services;

namespace N5Now.Infrastructure.Interfaces.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ILogger _logger;
        private readonly IProducer<Null, string> _producer;
        private const string TopicName = "permissions-topic-operations";

        public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
        {
            _logger = logger;

            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9069"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        
        public async Task PublishEventAsync(KafkaOperationsDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            await _producer.ProduceAsync(TopicName, new Message<Null, string> { Value = json });
        }
    }
}
