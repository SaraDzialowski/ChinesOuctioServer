using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Donators
{
    public interface IDonatorService
    {
        Task<IEnumerable<Donator>> GetDonators(int? id, string? name, string? email, string? giftName);
        Task<bool> AddDonator(Donator donator);
        Task<bool> UpdateDonator(Donator donator);
        Task<bool> DeleteDonator(int id);
        Task<IEnumerable<Gift>> GetDonatorGifts(int id);
    }
}
