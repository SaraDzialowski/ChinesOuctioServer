using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.Models;
using System.Collections.Generic;
using System.Threading;

namespace ChinesOuctionServer.DAL.Donators
{
    public class DonatorDal: IDonatorDal
    {
        private readonly HSContext _hsContext;
        private readonly IGiftDal _giftDal;

        public DonatorDal(HSContext hsContext,IGiftDal giftDal)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
            _giftDal = giftDal ?? throw new ArgumentNullException(nameof(giftDal));
        }

        public async Task<IEnumerable<Donator>> GetAsync(int? id, string? name, string? email, string? giftName)
        {
            var query = _hsContext.Donators.Where(donator =>
            ((id == null) ? (true) : (donator.Id == id))
            &&((name == null) ? (true) : (donator.FullName == name))
            && ((email == null) ? (true) : (donator.Email == email))
            && ((giftName == null) ? (true) : (donator.Id ==  _giftDal.GetAsync(null, giftName,null, null,null,null,null,null).Result.First().DonatorId))).Include(d => d.Gifts);
            List<Donator> donator = await query.ToListAsync();
            return donator;
        }
        public async Task<bool> AddAsync(Donator donator)
        {
           await _hsContext.Donators.AddAsync(donator);
           await _hsContext.SaveChangesAsync();
           return true;
        }
        public async Task<bool> UpdateAsync(Donator donator)
        {
            _hsContext.Donators.Update(donator);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            List<Gift> gifts = await _hsContext.Gifts.Where(g => g.DonatorId == id).ToListAsync();
            if (gifts.Count() >0)
                return false;
            Donator d = await _hsContext.Donators.FindAsync(id);
            _hsContext.Donators.Remove(d);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Gift>> GetDonatorGiftsAsync(int id)
        {
           return await _hsContext.Gifts.Where(g=> g.DonatorId == id).ToListAsync();
        }
    }
}
