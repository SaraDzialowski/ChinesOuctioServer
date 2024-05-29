using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Gifts
{
    public interface IGiftService
    {
        Task<IEnumerable<Gift>> GetGifts(int? id, string? name, string? donatorName, int? numOfPurcheses, int? price, EnumGiftCategory? category, bool? sortPrice, bool? maxQuentity);
        Task<bool> AddGift(Gift gift);
        Task<bool> UpdateGift(Gift gift);
        Task<bool> DeleteGift(int id);
    }
}
