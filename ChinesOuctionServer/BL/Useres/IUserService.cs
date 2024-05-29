using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChinesOuctionServer.BL.Useres
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers(int? id);
        Task<bool> AddUser(User purches);
        Task<bool> UpdateUser(User purches);
        Task<bool> DeleteUser(int id);
        Task<User> Authenticate(UserLogin userLogin);
        string Generate(User user);
    }
}
