using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ChinesOuctionServer.Models
{
    public class User
    {
        public int Id { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public EnumRole Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
