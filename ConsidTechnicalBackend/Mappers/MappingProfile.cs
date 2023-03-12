using AutoMapper;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLibraryItemRequest, DbLibraryItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<string, DbCategory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src));

        CreateMap<CreateEmployeeRequest, DbEmployees>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateEmployeeRequest, DbEmployees>();

        CreateMap< UpdateCategoryRequest, DbCategory>();
    
    }
}
