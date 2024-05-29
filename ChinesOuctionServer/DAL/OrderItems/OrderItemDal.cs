using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DAL.Orders;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.OrderItems
{
    public class OrderItemDal : IOrderItemDal
    {
        private readonly HSContext _hsContext;
        private readonly IGiftDal _giftDal;
        public OrderItemDal(HSContext hsContext, IGiftDal giftDal)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
            _giftDal = giftDal ?? throw new ArgumentNullException(nameof(giftDal));
        }
        public async Task<IEnumerable<OrderItem>> GetAsync(int? orderId)
        {
            var query = _hsContext.OrderItems.Where(oi =>
             ((orderId == null) ? (true) : (oi.OrderId == orderId && !oi.Flag)));
                List<OrderItem> orderItems = await query.ToListAsync();
             return orderItems;
        }
        public async Task<IEnumerable<Gift>> GetGiftAsync(int orderId)
        {
            List<OrderItem> orderItems = GetAsync(orderId).Result.ToList();
            List<Gift> gifts = new List<Gift>();
            foreach (var item in orderItems)
            {
                gifts.Add(_giftDal.GetAsync(item.GiftId, null, null, null, null, null, null, null).Result.First());
            }
            return gifts;
        }
        public async Task<bool> BuyAsync(int id)
        {
            Order o = await _hsContext.Orders.FindAsync(id);
            List<OrderItem> orderItems = GetAsync(id).Result.ToList();
            foreach (var oi in orderItems)
            {
                oi.Flag = true;
                _hsContext.OrderItems.Update(oi);
                o.Sum += _giftDal.GetAsync(oi.GiftId, null, null, null,null,null, null, null).Result.First().Price;
            }
            _hsContext.Orders.Update(o);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAsync(OrderItem orderItem)
        {
            Order o = await _hsContext.Orders.Where(o=>o.Id == orderItem.OrderId).Include(o=>o.OrderItems).FirstAsync();
            foreach (var oi in o.OrderItems)
            {
                if (oi.GiftId == orderItem.GiftId)
                {
                    oi.Quentity += 1;
                    await _hsContext.SaveChangesAsync();
                    return true;
                }
            }
            await _hsContext.OrderItems.AddAsync(orderItem);
            await _hsContext.SaveChangesAsync();
             
            return true;
        }
        public async Task<bool> UpdateAsync(OrderItem orderItem)
        {
            _hsContext.OrderItems.Update(orderItem);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            OrderItem oi = await _hsContext.OrderItems.FindAsync(id);
            if (oi.Flag == false) 
            {
                Gift g = _giftDal.GetAsync(oi.GiftId, null, null, null, null, null, null, null).Result.First();
                _hsContext.OrderItems.Remove(oi);
                await _hsContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
