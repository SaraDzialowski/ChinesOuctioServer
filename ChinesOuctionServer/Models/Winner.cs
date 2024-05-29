using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class Winner
    {
        [Key, NotNull]
        public int Id { get; set; }
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
