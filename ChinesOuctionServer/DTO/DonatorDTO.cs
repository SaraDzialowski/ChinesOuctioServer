using ChinesOuctionServer.Models;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.DTO
{
    public class DonatorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<GiftDTO> Gifts { get; set; }
    }
}
