using ChinesOuctionServer.DAL.Donators;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Donators
{
    public class DonatorService : IDonatorService
    {
        private readonly IDonatorDal _donatorDal;

        public DonatorService(IDonatorDal donatorDal)
        {
            _donatorDal = donatorDal ?? throw new ArgumentNullException(nameof(donatorDal));
        }

        public async Task<IEnumerable<Donator>> GetDonators(int? id, string? name, string? email, string? giftName)
        {
            return await _donatorDal.GetAsync(id,name,email,giftName);
        }
        public async Task<bool> AddDonator(Donator donator)
        {
            return await _donatorDal.AddAsync(donator);
        }
        public async Task<bool> UpdateDonator(Donator donator)
        {
            return await _donatorDal.UpdateAsync(donator);
        }
        public async Task<bool> DeleteDonator(int id)
        {
            return await _donatorDal.DeleteAsync(id);
        }
        public async Task<IEnumerable<Gift>> GetDonatorGifts(int id)
        {
            return await _donatorDal.GetDonatorGiftsAsync(id);
        }
    }
}
