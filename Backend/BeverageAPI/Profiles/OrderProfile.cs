﻿namespace BeverageAPI.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

        CreateMap<CustomerDTO, Customer>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
    }
}

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, OrderItemDTO>();
        CreateMap<OrderItemDTO, OrderItem>();
    }
}