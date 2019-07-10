using AutoMapper;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.OutputTemplateFields.Utility;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.DocuSign.Mapping
{
    public class TemplateFieldDtoAutoMapperConfigurator : Profile
    {
        public TemplateFieldDtoAutoMapperConfigurator()
        {
            CreateMap<Approve, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity => 
                    OutputTemplateFieldsConstants.FieldTypes.Approve));

            CreateMap<Checkbox, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Checkbox));

            CreateMap<Company, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Company));

            CreateMap<DateSigned, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.DateSigned));

            CreateMap<Date, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Date));

            CreateMap<Decline, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Decline));

            CreateMap<EmailAddress, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.EmailAddress));

            CreateMap<Email, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Email));

            CreateMap<EnvelopeId, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.EnvelopeId));

            CreateMap<FirstName, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.FirstName));

            CreateMap<FormulaTab, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Formula));

            CreateMap<FullName, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.FullName));

            CreateMap<InitialHere, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.InitialHere));

            CreateMap<LastName, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.LastName));

            CreateMap<List, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.List));

            CreateMap<Notarize, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Notarize));

            CreateMap<Note, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Note));

            CreateMap<Number, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Number));

            CreateMap<RadioGroup, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.RadioGroup));

            CreateMap<SignerAttachment, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.SignerAttachment));

            CreateMap<SignHere, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.SignHere));

            CreateMap<SmartSection, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Required,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.SmartSection));

            CreateMap<Ssn, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Ssn));

            CreateMap<Text, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Text));

            CreateMap<Title, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Title));

            CreateMap<View, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Locked,
                    options => options.MapFrom(entity => false))
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.View));

            CreateMap<Zip, TemplateFieldDto>()
                .ForMember(x => x.DocumentName, options => options.Ignore())
                .ForMember(x => x.Type,
                    options => options.MapFrom(entity =>
                    OutputTemplateFieldsConstants.FieldTypes.Zip));
        }
    }
}
