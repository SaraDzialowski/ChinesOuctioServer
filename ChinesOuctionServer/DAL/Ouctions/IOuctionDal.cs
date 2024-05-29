using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.Ouctions
{
    public interface IOuctionDal
    {
        Task<Winner> RaffleAsync (int giftId);
        Task<IEnumerable<Winner>> GetWinnersAsync(int? giftId);
        Task<string> GetSumAsync();
    }
}
