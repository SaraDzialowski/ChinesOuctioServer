using AutoMapper;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Mapper;
using ChinesOuctionServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ChinesOuctionServer.Migrations;
using ChinesOuctionServer.BL.Useres;

namespace ChinesOuctionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _user;
        private readonly IMapper _imapper;
        private readonly IConfiguration _config;

        public UserController(IUserService user, IMapper imapper,IConfiguration config)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get(int? id)
        {
            IEnumerable<User> users = await _user.GetUsers(id);
            IEnumerable<UserDTO> userDTOs = _imapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            foreach (User user in users)
            {
                IEnumerable<OrderDTO> orderDTOs = _imapper.Map<ICollection<Order>, IEnumerable<OrderDTO>>(user.Orders);
                userDTOs.First(d => d.Id == user.Id).Orders = orderDTOs.ToList();
            }

            return userDTOs;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register([FromBody] UserDTO user)
        {            
            if (user.Password == null || user.Email == null || user.Id == null || user.Phone == null || 
                user.UserName == null || user.FullName == null)
            {
                return NotFound("Details are missing");
            }
            else
            {  
                User u = _imapper.Map<UserDTO, User>(user);
                u.Role = EnumRole.user;
                return Ok(await _user.AddUser(u));
                
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin userLogin)
        {
            var user = await _user.Authenticate(userLogin);

            if (user != null)
            {
                object token = _user.Generate(user);
                var jsonToken = JsonConvert.SerializeObject(token);
                return Ok(new { jsonToken, user.Role,user.Id });
            }
            return Ok(null);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] UserDTO user)
        {
            User u = _imapper.Map<UserDTO, User>(user);
            return await _user.UpdateUser(u);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _user.DeleteUser(id);
        }
    }
}
