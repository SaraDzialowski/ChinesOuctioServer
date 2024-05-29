using ChinesOuctionServer.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChinesOuctionServer.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
