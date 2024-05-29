using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class Donator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Gift> Gifts { get; set; }
    }
}
