using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.OrderItems
{
    public interface IOrderItemDal
    {
        Task<IEnumerable<OrderItem>> GetAsync(int? orderId);
        Task<IEnumerable<Gift>> GetGiftAsync(int orderId);
        Task<bool> AddAsync(OrderItem order);
        Task<bool> UpdateAsync(OrderItem order);
        Task<bool> DeleteAsync(int id);
        Task<bool> BuyAsync(int id);
    }
}
