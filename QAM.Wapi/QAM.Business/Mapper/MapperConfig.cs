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

        CreateMap<CreateRoleRequest, Role>();
        CreateMap<Role, RoleResponse>();

        CreateMap<CreateContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

        CreateMap<CreateQuestionRequest, Question>();
        CreateMap<Question, QuestionResponse>()
            .ForMember(dest => dest.SubjectName,
                src => src.MapFrom(x => x.Subject.Name));
    }
}