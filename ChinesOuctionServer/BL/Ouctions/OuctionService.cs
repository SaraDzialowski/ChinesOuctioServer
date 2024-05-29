using ChinesOuctionServer.DAL.Orders;
using ChinesOuctionServer.DAL.Ouctions;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Ouctions
{
    public class OuctionService : IOuctionService

    {
        private readonly IOuctionDal _ouctionDal;

        public OuctionService(IOuctionDal ouctionDal)
        {
            _ouctionDal = ouctionDal ?? throw new ArgumentNullException(nameof(ouctionDal));
        }

        public async Task<Winner> Raffle(int giftId)
        {
            return await _ouctionDal.RaffleAsync(giftId);
        }

        public async Task<IEnumerable<Winner>> GetWinners(int? giftId)
        {
            return await _ouctionDal.GetWinnersAsync(giftId);
        }
        public async Task<string> GetSum()
        {
            return await _ouctionDal.GetSumAsync();
        }
    }
}
