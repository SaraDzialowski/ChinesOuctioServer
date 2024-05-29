using ChinesOuctionServer.Models;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public int Sum { get; set; } 
     public ICollection<OrderItem> OrderItems { get; set; }
    }
}