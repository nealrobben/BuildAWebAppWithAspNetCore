using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ForMember(x => x.OrderId, ex => ex.MapFrom(o => o.Id)).ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}
