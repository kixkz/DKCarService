using CarService.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace CarService.BL.Kafka
{
    public class Producer<TKey, TValue> : IKafkaProducerService<TKey, TValue>
    {
        private readonly IOptionsMonitor<KafkaProducerSettings> _kafkaSettings;
        private readonly IProducer<TKey, TValue> _producer;

        public Producer(IOptionsMonitor<KafkaProducerSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;

            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers
            };

            _producer = new ProducerBuilder<TKey, TValue>(config)
                .SetValueSerializer(new MsgPackSerializer<TValue>())
                .SetKeySerializer(new MsgPackSerializer<TKey>())
                .Build();
        }

        public async Task Produce(TKey messageKey, TValue messageValue)
        {
            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = messageKey,
                    Value = messageValue
                };

                var result = await _producer.ProduceAsync("Purchase", msg);

                if (result != null)
                {
                    Console.WriteLine($"Delivered: {result.Value} to {result.TopicPartitionOffset}");
                }
            }
            catch (ProduceException<int, string> ex)
            {
                Console.WriteLine($"Delivery failed: {ex.Error.Reason}");
            }
        }
    }
}
