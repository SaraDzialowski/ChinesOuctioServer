using ChinesOuctionServer.DAL.OrderItems;
using ChinesOuctionServer.DAL.Ouctions;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.OrderItems
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemDal _orderItemDal;

        public OrderItemService(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal ?? throw new ArgumentNullException(nameof(orderItemDal));
        }
        public async Task<IEnumerable<OrderItem>> Get(int? orderId)
        {
            return await _orderItemDal.GetAsync(orderId);
        }

        public async Task<bool> AddOrderItem(OrderItem order)
        {
            return await _orderItemDal.AddAsync(order);
        }

        public async Task<bool> UpdateOrderItem(OrderItem order)
        {
            return await _orderItemDal.UpdateAsync(order);
        }

        public async Task<bool> DeleteOrderItem(int id)
        {
            return await _orderItemDal.DeleteAsync(id);
        }

        public async Task<bool> BuyOrder(int id)
        {
            return await _orderItemDal.BuyAsync(id);
        }

        public async Task<IEnumerable<Gift>> GetGift(int orderId)
        {
            return await _orderItemDal.GetGiftAsync(orderId);
        }
    }
}
