using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Notes.Dtos;

namespace crmSeries.Core.Features.Notes.Mapping
{
    public class NotesMapperConfigurator : Profile
    {
        public NotesMapperConfigurator()
        {
            CreateMap<Note, GetNoteDto>();
            CreateMap<GetNoteDto, Note>();

            CreateMap<Note, EditNoteRequest>()
                .ForMember(dest => dest.TypeId, options => options.MapFrom(src => src.TypeId));

            CreateMap<EditNoteRequest, Note>()
                .ForMember(dest => dest.TypeId, options => options.MapFrom(src => src.TypeId));

            CreateMap<AddNoteRequest, Note>()
                .ForMember(x => x.Deleted, options => options.Ignore())
                .ForMember(x => x.LastModified, options => options.Ignore());

            CreateMap<EditNoteRequest, Task>()
                .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}