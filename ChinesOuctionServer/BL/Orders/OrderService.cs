using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DAL.Orders;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderService(IOrderDal orderDal)
        {
            _orderDal = orderDal ?? throw new ArgumentNullException(nameof(orderDal));
        }
        public async Task<IEnumerable<Order>> Get(int? id)
        {
            return await _orderDal.GetAsync(id);
        }

        public async Task<Order> GetLastOrder(int userId)
        {
            return await _orderDal.GetLastOrderAsync(userId);
        }
        public async Task<int> AddOrder(Order order)
        {
            return await _orderDal.AddAsync(order);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            return await _orderDal.UpdateAsync(order);
        }

        public async Task<int> BuyOrder(int id)
        {
            return await _orderDal.BuyAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetGiftCards(string giftName)
        {
            return await _orderDal.GetGiftCardsAsync(giftName);
        }

        public async Task<IEnumerable<Gift>> GetSortGifts(bool? price, bool? maxQuentity)
        {
            return await _orderDal.GetSortGiftsAsync(price, maxQuentity);
        }

        public async Task<IEnumerable<UserDTO>> GetPurchesesDetails()
        {
            return await _orderDal.GetPurchesesDetailsAsync();
        }

        
    }
}
