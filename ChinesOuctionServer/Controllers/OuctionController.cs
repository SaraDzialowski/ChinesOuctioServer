using AutoMapper;
using ChinesOuctionServer.BL.Orders;
using ChinesOuctionServer.BL.Ouctions;
using ChinesOuctionServer.Mapper;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChinesOuctionServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OuctionController : ControllerBase
    {
        private readonly IOuctionService _ouction;
        private readonly IMapper _imapper;

        public OuctionController(IOuctionService ouction, IMapper imapper)
        {
            _ouction = ouction ?? throw new ArgumentNullException(nameof(ouction));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("{giftId}")]
        public async Task<Winner> Get(int giftId)
        {
            return await _ouction.Raffle(giftId);
        }
   
        [HttpGet("GetWinners")]
        public async Task<IEnumerable<Winner>> GetWinners(int? giftId)
        {
            return await _ouction.GetWinners(giftId);
        }
        [HttpGet("GetSum")]
        public async Task<string> GetSum()
        {
            return await _ouction.GetSum();
        }

    }
}
