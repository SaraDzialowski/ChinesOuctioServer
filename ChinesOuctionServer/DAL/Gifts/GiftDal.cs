using ChinesOuctionServer.DAL.Donators;
using ChinesOuctionServer.DAL.Orders;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ChinesOuctionServer.DAL.Gifts
{
    public class GiftDal : IGiftDal
    {
        private readonly HSContext _hsContext;
        public GiftDal(HSContext hsContext)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
        }

        public async Task<IEnumerable<Gift>> GetAsync(int? id, string? name, string? donatorName, int? numOfPurcheses,int? price, EnumGiftCategory? category, bool? sortPrice, bool? maxQuentity)
        {
            IQueryable<Gift> query = _hsContext.Gifts;
            if (sortPrice != true && maxQuentity != true)
            {
                  query = _hsContext.Gifts.Where(gift =>
                            ((id == null) ? (true) : (gift.Id == id))
                            &&((name == null) ? (true) : (gift.Name == name))
                            && ((donatorName == null) ? (true) : (gift.DonatorId == GetDonatorIdByName(donatorName).Result))
                            && ((numOfPurcheses == null) ? (true) : (gift.Count == numOfPurcheses))
                            && ((category == null) ? (true) : (gift.Category == category))
                            && ((price == null) ? (true) : (gift.Price == price)));
                            
            }
            else
            {
                if ((bool)sortPrice)
                {
                    query = query.OrderByDescending(g => g.Price);
                }
                else if ((bool)maxQuentity)
                {
                    query = query.OrderByDescending(g => g.Count);
                }
                return await query.ToListAsync();
            }


            List<Gift> gifts = await query.ToListAsync();
            return gifts;
        }
        public async Task<bool> AddAsync(Gift gift)
        {
            //if ((_hsContext.Gifts.Where(g => g.Name == gift.Name)) != null)
            //    return false;
            await _hsContext.Gifts.AddAsync(gift);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Gift gift)
        {
            _hsContext.Gifts.Update(gift);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            List<OrderItem> raffleItems = await _hsContext.OrderItems.Where(oi => oi.GiftId == id).ToListAsync();
            if (raffleItems.Count() > 0)
                return false;
            Gift g = await _hsContext.Gifts.FindAsync(id);
            _hsContext.Gifts.Remove(g);
            await _hsContext.SaveChangesAsync();
            return true;
        }

        private async Task<int> GetDonatorIdByName(string name)
        {
            IEnumerable<Donator> d =_hsContext.Donators.Where(d => d.FullName == name).ToList();
            if (d.Count() > 0)
                return d.First().Id;
            else
                return 0;

        }
        
    }
}
