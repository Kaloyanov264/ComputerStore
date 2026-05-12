namespace ComputerStore.BL.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceSaleMessageAsync(int key, string message);
    }
}
