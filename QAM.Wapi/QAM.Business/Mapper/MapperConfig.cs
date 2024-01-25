using AutoMapper;

using QAM.Data.Entity;
using QAM.Scheme;

namespace QAM.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {

        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.RoleName,
                src => src.MapFrom(x => x.Role.Name));



    }
}