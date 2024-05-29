using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Ouctions
{
    public interface IOuctionService
    {
        Task<Winner> Raffle(int giftId);
        Task<IEnumerable<Winner>> GetWinners(int? giftId);
        Task<string> GetSum();
    }
}
