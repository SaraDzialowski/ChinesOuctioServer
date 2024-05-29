using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DAL.Users;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ChinesOuctionServer.DAL.Ouctions
{

    public class OuctionDal : IOuctionDal
    {
        private readonly HSContext _hsContext;

        public OuctionDal(HSContext hsContext)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
        }

        public async Task<Winner> RaffleAsync(int giftId)
        {
            Gift gift = await _hsContext.Gifts.Where(g => g.Id == giftId).FirstAsync();
            List<OrderItem> TypeItems = await _hsContext.OrderItems.Where(oi => oi.GiftId == giftId && oi.Flag).ToListAsync();
            List<OrderItem> raffleItems = new List<OrderItem>();
            for (int i = 0; i < TypeItems.Count; i++)
            {
                for (int j = 1; j <= TypeItems[i].Quentity; j++)
                {
                    raffleItems.Add(TypeItems[i]);
                }
            }
            if (raffleItems.Count > 0)
            {
                Random r = new Random();
                int num = r.Next(0, raffleItems.Count);
                OrderItem winItem = raffleItems[num];
                int winnerId = await _hsContext.Orders.Where(o => o.Id == winItem.OrderId).Select(o => o.UserId).FirstAsync();
                Winner winner = new Winner()
                {
                    GiftId = giftId,
                    UserId = winnerId
                };
                gift.Status = true;
                await _hsContext.SaveChangesAsync();
                 await _hsContext.Winner.AddAsync(winner);
                await _hsContext.SaveChangesAsync();
                return winner;
            }
            return null;

        }
        public async Task<IEnumerable<Winner>> GetWinnersAsync(int? giftId)
        {
            List<Winner> winners;
            string filePath = "M://ChinesOuctionProject👍👍👍//Winners.txt";
            if (giftId != null)     
                 winners = await _hsContext.Winner.Where(w => w.GiftId == giftId).ToListAsync();
            else
                 winners = await _hsContext.Winner.ToListAsync();
            List<Gift> gifts = await _hsContext.Gifts.ToListAsync();
            List<User> users = await _hsContext.Users.ToListAsync();
            var result = winners.Select(w =>
            {
                var gift = gifts.FirstOrDefault(p => p.Id == w.GiftId);
                var user = users.FirstOrDefault(u => u.Id == w.UserId);

                return $"Present: {gift?.Name}, Winner: {user?.FullName}";
            });
            string text = string.Join(Environment.NewLine, result);
            if (giftId == null)
            {
                await File.WriteAllTextAsync(filePath, text);
                Process.Start("notepad.exe", filePath);
            }           
            return winners;
        }
        public async Task<string> GetSumAsync()
        {
            string filePath = "M://ChinesOuctionProject👍👍👍//Results.txt";
            List<Order> orders = await _hsContext.Orders.Where(o => o.Sum > 0)?.ToListAsync();
            int sum = 0;
            var result = orders.Select(w =>
            {
                sum += w.Sum;
                return $"Order: {w.Id}, Sum: {w.Sum}";
            }); 
            string text = string.Join(Environment.NewLine, result);
            text += string.Join("sum: ",Environment.NewLine,sum);
            await File.WriteAllTextAsync(filePath, text);
            Process.Start("notepad.exe", filePath);
            return null;
        }                                                                                                                                                                         

    }
}
