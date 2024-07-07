namespace CandyStoreApi.Models
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
    }
}
