using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChinesOuctionServer.Models
{
    public class Raffle
    {
        [Key, NotNull]
        public int Id { get; set; }
        public DateTime RaffleDate { get; set; }

    }
}
