﻿using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(o => o.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(o => o.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
