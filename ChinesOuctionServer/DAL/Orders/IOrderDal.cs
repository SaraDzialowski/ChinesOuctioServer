using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.Orders
{
    public interface IOrderDal
    {

        Task<IEnumerable<Order>> GetAsync(int? id);
        Task<Order> GetLastOrderAsync(int userId);
        Task<int> AddAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<int> BuyAsync(int id);
        Task<IEnumerable<OrderItem>> GetGiftCardsAsync(string giftName);
        Task<IEnumerable<Gift>> GetSortGiftsAsync(bool? price, bool? maxQuentity);
        Task<IEnumerable<UserDTO>> GetPurchesesDetailsAsync();

    }
}
