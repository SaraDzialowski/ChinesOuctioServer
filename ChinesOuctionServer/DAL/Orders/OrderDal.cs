using AutoMapper;
using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace ChinesOuctionServer.DAL.Orders
{
    public class OrderDal : IOrderDal
    {
        private readonly HSContext _hsContext;
        private readonly IGiftDal _giftDal;
        private readonly IMapper _imapper;
        public OrderDal(HSContext hsContext, IMapper imapper, IGiftDal giftDal)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
            _giftDal = giftDal ?? throw new ArgumentNullException(nameof(giftDal));
        }
        public async Task<IEnumerable<Order>> GetAsync(int? id)
        {
            var query = _hsContext.Orders.Where(order =>
             ((id == null) ? (true) : (order.Id == id))).Include(o => o.OrderItems);
            List<Order> order = await query.ToListAsync();
            return order;
        }
        public async Task<Order> GetLastOrderAsync(int userId)
        {
            var query = _hsContext.Orders.Where(o => o.UserId == userId);
            if (query.Count() == 0)
                return null;
            Order o = await _hsContext.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.Date).FirstAsync();
            return o;
        }

        public async Task<int> AddAsync(Order order)
        {
            await _hsContext.Orders.AddAsync(order);
            await _hsContext.SaveChangesAsync();
            return order.Id;
        }
        public async Task<bool> UpdateAsync(Order order)
        {
            _hsContext.Orders.Update(order);
            await _hsContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetGiftCardsAsync(string giftName)
        {
            int giftId = _giftDal.GetAsync(null, giftName, null, null, null, null, null, null).Result.First().Id;
            return await _hsContext.OrderItems.Where(oi => oi.GiftId == giftId && oi.Flag).ToListAsync();
        }

        public async Task<IEnumerable<Gift>> GetSortGiftsAsync(bool? price, bool? maxQuentity)
        {
            IQueryable<Gift> query = _hsContext.Gifts;
            if ((bool)price)
            {
                query = query.OrderByDescending(g => g.Price);
            }
            else if ((bool)maxQuentity)
            {
                query = query.OrderByDescending(g => g.Count);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<UserDTO>> GetPurchesesDetailsAsync()
        {
            string filePath = "M://ChinesOuctionProject👍👍👍//PurchesesDetails.txt";
            List<User> users = await _hsContext.Users.Where(u => u.Orders.First().Sum > 0).ToListAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();
            for (int i = 0; i < users.Count; i++)
            {
                usersDTO.Add(_imapper.Map<User, UserDTO>(users[i]));
            }
            var result = usersDTO.Select(u =>
            {
                return $"FullName: {u.FullName}, :Email {u.Email}";
            });
            string text = string.Join(Environment.NewLine, result);
            await File.WriteAllTextAsync(filePath, text);
            Process.Start("notepad.exe", filePath);
            return usersDTO;
        }
        public async Task<int> BuyAsync(int id)
        {
            Order o = await _hsContext.Orders.Where(o => o.Id == id).Include(o => o.OrderItems).FirstAsync();

            foreach (var oi in o?.OrderItems)
            {
                Gift gift = _giftDal.GetAsync(oi.GiftId, null, null, null, null, null, null, null).Result.First();
                gift.Count += oi.Quentity;
                oi.Flag = true;
                _hsContext.OrderItems.Update(oi);
                o.Sum += _giftDal.GetAsync(oi.GiftId, null, null, null, null, null, null, null).Result.First().Price * oi.Quentity;
            }
            _hsContext.Orders.Update(o);
            await _hsContext.SaveChangesAsync();
            return o.Sum;
        }
      
    }
}
