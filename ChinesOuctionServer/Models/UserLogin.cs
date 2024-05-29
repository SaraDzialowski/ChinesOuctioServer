using System.Diagnostics.CodeAnalysis;

namespace ChinesOuctionServer.Models
{
    public class UserLogin
    {
        [NotNull]
        public string UserName { get; set; }

        [NotNull]
        public string Password { get; set; }

    }
}
