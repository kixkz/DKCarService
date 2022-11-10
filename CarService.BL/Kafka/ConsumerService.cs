using System.Text;
using System.Threading.Tasks.Dataflow;
using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarService.BL.Kafka
{
    public class ConsumerService<TKey, TValue> : IHostedService
    {
        private IOptionsMonitor<KafkaConsumerSettings> _kafkaConsumerSettings;
        private readonly IConsumer<TKey, TValue> _consumer;
        private readonly TransformBlock<TValue, string> _purchaseTransformBlock;
        private readonly ActionBlock<string> _actionBlock;
        private readonly ITyreRepo _tyreRepo;

        public ConsumerService(IOptionsMonitor<KafkaConsumerSettings> kafkaSettings, ITyreRepo tyreRepo)
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

            _purchaseTransformBlock = new TransformBlock<TValue, string>(async purchase =>
            {
                return purchase.ToString();
            });

            _actionBlock = new ActionBlock<string>(str =>
            {
                Console.WriteLine(str);
            });

            _purchaseTransformBlock.LinkTo(_actionBlock);
            _tyreRepo = tyreRepo;
        }

        public void Consume()
        {
            _consumer.Subscribe("Purchase");
            while (true)
            {
                var cr = _consumer.Consume();

                _purchaseTransformBlock.Post(cr.Value);

                //Console.WriteLine($"Receiver msg with key:{cr.Message.Key} value:{cr.Message.Value}");
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
