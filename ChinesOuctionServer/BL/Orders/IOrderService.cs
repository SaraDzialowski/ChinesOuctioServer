using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> Get(int? id);
        Task<Order> GetLastOrder(int userId);
        Task<int> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<int> BuyOrder(int id);
        Task<IEnumerable<OrderItem>> GetGiftCards(string giftName);
        Task<IEnumerable<Gift>> GetSortGifts(bool? price, bool? maxQuentity);
        Task<IEnumerable<UserDTO>> GetPurchesesDetails();
    }
}
