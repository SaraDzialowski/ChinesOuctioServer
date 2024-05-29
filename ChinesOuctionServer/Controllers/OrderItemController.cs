using AutoMapper;
using ChinesOuctionServer.BL.OrderItems;
using ChinesOuctionServer.BL.Orders;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChinesOuctionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItem;
        private readonly IMapper _imapper;

        public OrderItemController(IOrderItemService orderItem, IMapper imapper)
        {
            _orderItem = orderItem ?? throw new ArgumentNullException(nameof(orderItem));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }
        [HttpGet]
        public async Task<IEnumerable<OrderItem>> Get(int? orderId)
        {
            return await _orderItem.Get(orderId);
        }
        [HttpGet("GetGift{orderId}")]
        public async Task<IEnumerable<Gift>> GetGift(int orderId)
        {
            return await _orderItem.GetGift(orderId);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] OrderItemDTO orderItem)
        {
            OrderItem oi = _imapper.Map<OrderItemDTO, OrderItem>(orderItem);
            return await _orderItem.AddOrderItem(oi);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] OrderItemDTO orderItem)
        {
            OrderItem oi = _imapper.Map<OrderItemDTO, OrderItem>(orderItem);
            return await _orderItem.UpdateOrderItem(oi);
        }
        [HttpPut("Buy{id}")]
        public async Task<ActionResult<bool>> Buy(int id)
        {
            return await _orderItem.BuyOrder(id);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _orderItem.DeleteOrderItem(id);
        }
    }
}
