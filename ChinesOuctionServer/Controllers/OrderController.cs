using AutoMapper;
using ChinesOuctionServer.BL.Orders;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Mapper;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ChinesOuctionServer.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _order;
        private readonly IMapper _imapper;

        public OrderController(IOrderService order, IMapper imapper)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IEnumerable<OrderDTO>> Get(int? id)
        {

            IEnumerable<Order> orders = await _order.Get(id);
            IEnumerable<OrderDTO> orderDTOs = _imapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);

            foreach (Order order in orders)
            {
                IEnumerable<OrderItemDTO> oiDTOs = _imapper.Map<ICollection<OrderItem>, IEnumerable<OrderItemDTO>>(order.OrderItems);
                orderDTOs.First(o => o.Id == order.Id).OrderItems = oiDTOs.ToList();
            }

            return orderDTOs;
        }
        [HttpGet("GetLastOrder")]
        public async Task<ActionResult<Order>> GetLastOrder()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (int.TryParse(userIdClaim?.Value, out int userId))
                {
                    return await _order.GetLastOrder(userId);
                }
                else
                {
                    return BadRequest("There is inValid userId.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] OrderDTO order)
        {
            try
            {
                Order o = _imapper.Map<OrderDTO, Order>(order);
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (int.TryParse(userIdClaim?.Value, out int userId))
                {
                    o.UserId = userId;
                    return await _order.AddOrder(o);
                }
                else
                {
                    return BadRequest("There is inValid userId.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Buy/{id}")]
        public async Task<ActionResult<int>> Buy(int id)
        {
            return await _order.BuyOrder(id);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] OrderDTO order)
        {
            Order o = _imapper.Map<OrderDTO, Order>(order);
            return await _order.UpdateOrder(o);
        }

        [HttpGet("GetGiftCards/{giftName}")]
        public async Task<IEnumerable<OrderItem>> Get(string giftName)
        {
            return await _order.GetGiftCards(giftName);
        }
        [HttpGet("SortGift")]
        public async Task<IEnumerable<Gift>> Get(bool? price, bool? maxQuentity)
        {
            return await _order.GetSortGifts(price, maxQuentity);
        }
        [HttpGet("GetPurchesesDetails")]
        public async Task<IEnumerable<UserDTO>> GetPurchesesDetails()
        {
            return await _order.GetPurchesesDetails();
        }
    }
}
