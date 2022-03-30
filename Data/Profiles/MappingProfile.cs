using AutoMapper;
using testAutofac.Data.Entities;
using testAutofac.Data.Requests;

namespace testAutofac.Data.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePermissionRequest, Permission>();
        CreateMap<UpdatePermissionRequest, Permission>();
    }
}