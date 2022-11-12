using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductOrigin, o => o.MapFrom(s => s.ProductOrigin.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<ProductOrigin, OriginForReturnDto>();
            CreateMap<ProductType, ProductToReturnDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<OriginDto, ProductOrigin>().ReverseMap();
            CreateMap<TypeDto, ProductType>().ReverseMap();

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<BasketDto, Basket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.Shipping, o => o.MapFrom(s => s.Shipping.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.Shipping.Price));

            CreateMap<OrderItem, OrderItemDto>()
             .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
             .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}