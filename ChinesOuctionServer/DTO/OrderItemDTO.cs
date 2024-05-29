using ChinesOuctionServer.Models;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GiftId { get; set; }
        public int Quentity { get; set; }
        public bool Flag { get; set; }
    }
}
