using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL.Users
{
    public interface IUserDal
    {
        Task<IEnumerable<User>> GetAsync(int? id);
        Task<bool> AddAsync(User purches);
        Task<bool> UpdateAsync(User purches);
        Task<bool> DeleteAsync(int id);
        Task<User> AuthenticateAsync(UserLogin userLogin);
    }
}
