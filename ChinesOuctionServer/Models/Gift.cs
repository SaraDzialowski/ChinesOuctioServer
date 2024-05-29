using ChinesOuctionServer.Models;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class Gift
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int DonatorId { get; set; }
        public Donator Donator { get; set; }
        public EnumGiftCategory Category { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Price { get; set; } = 10;
        public int Count { get; set; }
        public bool Status { get; set; } = false;
    }
}
