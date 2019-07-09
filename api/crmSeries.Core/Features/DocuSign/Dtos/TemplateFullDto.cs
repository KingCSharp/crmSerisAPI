using crmSeries.Core.Features.DocuSign.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.DocuSign.Dtos
{
    public class BaseTemplateFullDto : DocuSignTemplate
    {
    }

    public class GetTemplateFullDto : BaseTemplateFullDto
    {
        public List<TemplateFieldDto> Fields { get; set; } = new List<TemplateFieldDto>();
        public List<TemplateRoleDto> Roles { get; set; } = new List<TemplateRoleDto>();
    }
}
