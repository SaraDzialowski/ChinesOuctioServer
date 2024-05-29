using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.OrderItems
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> Get(int? orderId);
        Task<IEnumerable<Gift>> GetGift(int orderId);
        Task<bool> AddOrderItem(OrderItem order);
        Task<bool> UpdateOrderItem(OrderItem order);
        Task<bool> DeleteOrderItem(int id);
        Task<bool> BuyOrder(int id);
    }
}
