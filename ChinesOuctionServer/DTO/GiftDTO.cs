using ChinesOuctionServer.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.DTO
{
    public class GiftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DonatorId { get; set; }
        public EnumGiftCategory Category { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public bool Status { get; set; }
    }
}
