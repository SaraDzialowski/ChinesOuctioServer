using ChinesOuctionServer.DAL;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public int Quentity { get; set; } = 1;
        public bool Flag { get; set; } = false;
    }
}
