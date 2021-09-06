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
            CreateMap<Product, ProductDto>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.SubCategory.Name))
            .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<SubCategory, SubCategoryDto>()
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
            .ForMember( d=> d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember( d=> d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
            .ForMember( d=> d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember( d=> d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember( d=> d.ImageUrl, o => o.MapFrom(s => s.ItemOrdered.ImageUrl))
            .ForMember( d => d.ImageUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}