using ChinesOuctionServer.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChinesOuctionServer.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
