using ChinesOuctionServer.DAL.Users;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Drawing;
using ChinesOuctionServer.Migrations;
using System.Data;

namespace ChinesOuctionServer.BL.Useres
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _config;

        public UserService(IUserDal userDal, IConfiguration config)
        {
            _userDal = userDal ?? throw new ArgumentNullException(nameof(userDal));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<IEnumerable<User>> GetUsers(int? id)
        {
            return await _userDal.GetAsync(id);
        }
        public async Task<bool> AddUser(User user)
        {
            return await _userDal.AddAsync(user);
        }
        public async Task<bool> UpdateUser(User user)
        {
            return await _userDal.UpdateAsync(user);
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await _userDal.DeleteAsync(id);
        }

        public async Task<User> Authenticate(UserLogin userLogin)
        {
            return await _userDal.AuthenticateAsync(userLogin);
        }
        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim("email", username),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
                 new Claim("Id",user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "http://localhost:7171/",
                Audience = "http://localhost:4200",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }
    }
}
