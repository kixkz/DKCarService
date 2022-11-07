using CarService.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarService.BL.Kafka
{
    public class ConsumerService<TKey, TValue> : IKafkaConsumerService<TKey, TValue>,IHostedService
    {
        private IOptionsMonitor<KafkaConsumerSettings> _kafkaConsumerSettings;
        private readonly IConsumer<TKey, TValue> _consumer;

        public ConsumerService(IOptionsMonitor<KafkaConsumerSettings> kafkaSettings)
        {
            _kafkaConsumerSettings = kafkaSettings;

            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaConsumerSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "MyGroup"
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config)
                .SetValueDeserializer(new MsgPackDeserializer<TValue>())
                .SetKeyDeserializer(new MsgPackDeserializer<TKey>())
                .Build();
        }

        public void Consume()
        {
            _consumer.Subscribe("CarTopic");
            while (true)
            {
                var cr = _consumer.Consume();

                Console.WriteLine($"Receiver msg with key:{cr.Message.Key} value:{cr.Message.Value}");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => Consume());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();

            return Task.CompletedTask;
        }
    }
}
