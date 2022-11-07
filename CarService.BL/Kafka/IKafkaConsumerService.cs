namespace CarService.BL.Kafka
{
    public interface IKafkaConsumerService<TKey, TValue>
    {
        void Consume();
    }
}
