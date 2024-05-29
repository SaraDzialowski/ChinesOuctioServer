using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ChinesOuctionServer.DAL.Users
{
    public class UserDal: IUserDal
    {
        private readonly HSContext _hsContext;

        public UserDal(HSContext hsContext)
        {
            _hsContext = hsContext ?? throw new ArgumentNullException(nameof(hsContext));
        }

        public async Task<IEnumerable<User>> GetAsync(int? id)
        {
            var query = _hsContext.Users.Where(user =>
            ((id == null) ? (true) : (user.Id == id))).Include(u => u.Orders).ThenInclude(o=>o.OrderItems).ToListAsync();;
            List<User> users = await query;
            return users;
        }
        public async Task<bool> AddAsync(User user)
        {
            await _hsContext.Users.AddAsync(user);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(User purches)
        {
            _hsContext.Users.Update(purches);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            User p = await _hsContext.Users.FindAsync(id);
            _hsContext.Users.Remove(p);
            await _hsContext.SaveChangesAsync();
            return true;
        }
        public async Task<User> AuthenticateAsync(UserLogin userLogin)
        {
            var currentUser = _hsContext.Users.FirstOrDefault(u => u.UserName.ToLower() ==
            userLogin.UserName.ToLower() && u.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
