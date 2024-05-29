using AutoMapper;
using ChinesOuctionServer.DTO;
using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Gift, GiftDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO,User>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Donator, DonatorDTO>().ReverseMap();
        }
    }
}
