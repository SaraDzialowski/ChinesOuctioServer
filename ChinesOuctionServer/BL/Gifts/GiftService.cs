using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.BL.Gifts
{
    public class GiftService: IGiftService
    {
        private readonly IGiftDal _giftDal;

        public GiftService(IGiftDal giftDal)
        {
            _giftDal = giftDal ?? throw new ArgumentNullException(nameof(giftDal));
        }

        public async Task<IEnumerable<Gift>> GetGifts(int? id, string? name, string? donatorName, int? numOfPurcheses, int? price, EnumGiftCategory? category, bool? sortPrice, bool? maxQuentity)
        {
            return await _giftDal.GetAsync(id,name,donatorName,numOfPurcheses,price,category,sortPrice, maxQuentity);
        }
        public async Task<bool> AddGift(Gift gift)
        {
            return await _giftDal.AddAsync(gift);
        }
        public async Task<bool> UpdateGift(Gift gift)
        {
            return await _giftDal.UpdateAsync(gift);
        }
        public async Task<bool> DeleteGift(int id)
        {
            return await _giftDal.DeleteAsync(id);
        }
    }
}
