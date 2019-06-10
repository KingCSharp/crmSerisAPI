using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyAssignedCategories.Dtos;

namespace crmSeries.Core.Features.CompanyAssignedCategories.Mapping
{
    public class CompanyAssignedCategoryDtoAutoMapperConfigurator : Profile
    {
        public CompanyAssignedCategoryDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyAssignedCategory, GetCompanyAssignedCategoryDto>();
            CreateMap<GetCompanyAssignedCategoryDto, CompanyAssignedCategory>();

            CreateMap<AddCompanyAssignedCategoryRequest, CompanyAssignedCategory>()
                .ForMember(x => x.AssignedId, options => options.Ignore());
        }
    }
}