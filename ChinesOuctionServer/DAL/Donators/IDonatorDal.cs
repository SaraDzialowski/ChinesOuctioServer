using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.Donators
{
    public interface IDonatorDal
    {
        Task<IEnumerable<Donator>> GetAsync(int? id, string? name, string? email, string? giftName);
        Task<bool> AddAsync(Donator donator);
        Task<bool> UpdateAsync(Donator donator);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Gift>> GetDonatorGiftsAsync(int id);
    }
}
