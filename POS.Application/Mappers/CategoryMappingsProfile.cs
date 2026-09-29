using AutoMapper;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Application.Mappers;

public class CategoryMappingsProfile : Profile
{

    public CategoryMappingsProfile()
    {
        //CreateMap<Category, CategoryResponseDto>()
        //    .ForMember(dest => dest.StateCategory, opt => 
        //        opt.MapFrom(src => src.State.Equals((int)StateTypes.Active) ? "Active" : "Inactive"))
        //    .ReverseMap();

        CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

        CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
            .ReverseMap(); 
        
        CreateMap<Category, CategorySelectResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<Category, CategoryRequestDto>()
            .ReverseMap();
    }
}
