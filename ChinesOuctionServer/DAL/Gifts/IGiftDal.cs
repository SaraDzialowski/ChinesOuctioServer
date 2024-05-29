using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.Gifts
{
    public interface IGiftDal
    {
        Task<IEnumerable<Gift>> GetAsync(int? id, string? name, string? donatorName, int? numOfPurcheses, int? price, EnumGiftCategory? category, bool? sortPrice, bool? maxQuentity);
        Task<bool> AddAsync(Gift gift);
        Task<bool> UpdateAsync(Gift gift);
        Task<bool> DeleteAsync(int id);
    }
}
