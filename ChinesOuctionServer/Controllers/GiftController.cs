using AutoMapper;
using ChinesOuctionServer.BL.Donators;
using ChinesOuctionServer.BL.Gifts;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChinesOuctionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController: ControllerBase
    {
        private readonly IGiftService _gift;
        private readonly IMapper _imapper;
            
        public GiftController(IGiftService gift, IMapper imapper)
        {
            _gift = gift ?? throw new ArgumentNullException(nameof(gift));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet]
        public async Task<IEnumerable<GiftDTO>> Get(int? id,string? name, string? donatorName, int? numOfPurcheses, int? price, EnumGiftCategory? category, bool? sortPrice, bool? maxQuentity)
        {
            IEnumerable<Gift> gifts = await _gift.GetGifts(id, name, donatorName, numOfPurcheses, price, category, sortPrice, maxQuentity);
            IEnumerable<GiftDTO> giftsDTO = _imapper.Map<ICollection<Gift>, IEnumerable<GiftDTO>>(gifts.ToList());
            return giftsDTO;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] GiftDTO gift)
        {
            Gift g = _imapper.Map<GiftDTO,Gift>(gift);
            return await _gift.AddGift(g);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] GiftDTO gift)
        {
            Gift g = _imapper.Map<GiftDTO, Gift>(gift);
            return await _gift.UpdateGift(g);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _gift.DeleteGift(id);
        }
    }
}
