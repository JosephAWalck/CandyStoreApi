namespace CandyStoreApi.Models
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(OrderDTO orderDTO);
    }
}
