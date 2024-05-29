using AutoMapper;
using ChinesOuctionServer.BL.Donators;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChinesOuctionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatorController : ControllerBase
    {
        private readonly IDonatorService _donator;
        private readonly IMapper _imapper;

        public DonatorController(IDonatorService donator, IMapper imapper)
        {
            _donator = donator ?? throw new ArgumentNullException(nameof(donator));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet]
        public async Task<IEnumerable<DonatorDTO>> Get(int? id, string? name, string? email, string? giftName)
        {
            IEnumerable<Donator> donators = await _donator.GetDonators(id, name, email, giftName);
            IEnumerable<DonatorDTO> donarDTOs = _imapper.Map<IEnumerable<Donator>, IEnumerable<DonatorDTO>>(donators);

            foreach (Donator donator in donators)
            {
                IEnumerable<GiftDTO> presentDTOs = _imapper.Map<ICollection<Gift>, IEnumerable<GiftDTO>>(donator.Gifts);
                donarDTOs.First(d => d.Id == donator.Id).Gifts = presentDTOs.ToList();
            }
            return donarDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] DonatorDTO donator)
        {
            try
            {
                Donator d = _imapper.Map<DonatorDTO, Donator>(donator);
                return await _donator.AddDonator(d);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] DonatorDTO donator)
        {
            Donator d = _imapper.Map<DonatorDTO, Donator>(donator);
            return await _donator.UpdateDonator(d);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _donator.DeleteDonator(id);
        }

        [HttpGet("GetDonatorGifts/{id}")]
        public async Task<IEnumerable<Gift>> GetDonatorGifts(int id)
        {
            return await _donator.GetDonatorGifts(id);
        }
    }
}
