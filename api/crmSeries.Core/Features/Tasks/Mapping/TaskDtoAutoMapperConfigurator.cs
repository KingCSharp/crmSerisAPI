using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Features.Tasks.Dtos;

namespace crmSeries.Core.Features.Tasks.Mapping
{
    public class TaskDtoAutoMapperConfigurator : Profile
    {
        public TaskDtoAutoMapperConfigurator()
        {
            CreateMap<Task, GetTaskDto>();
            CreateMap<GetTaskDto, Task>();

            CreateMap<AddTaskRequest, Task>()
                .ForMember(x => x.ContactId, options => options.Ignore())
                .ForMember(x => x.Deleted, options => options.Ignore())
                .ForMember(x => x.LastModified, options => options.Ignore());

            CreateMap<EditTaskRequest, Task>()
                .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}