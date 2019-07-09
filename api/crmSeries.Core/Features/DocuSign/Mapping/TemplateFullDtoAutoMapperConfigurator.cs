using AutoMapper;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.DocuSign.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.DocuSign.Mapping
{
    public class TemplateFullDtoAutoMapperConfigurator : Profile
    {
        public TemplateFullDtoAutoMapperConfigurator()
        {
            CreateMap<DocuSignTemplate, GetTemplateFullDto>()
                .ForMember(x => x.Fields, options => options.Ignore());

            CreateMap<GetTemplateFullDto, DocuSignTemplate>();
        }
    }
}
