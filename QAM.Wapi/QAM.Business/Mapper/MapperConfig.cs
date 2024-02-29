using AutoMapper;
using QAM.Base.Encryption;
using QAM.Data.Entity;
using QAM.Scheme;

namespace QAM.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CreateUserRequest, User>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Md5Extension.GetHash(src.Password.Trim())));
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

        CreateMap<CreateSubjectRequest, Subject>();
        CreateMap<Subject, SubjectResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

        CreateMap<CreateTagRequest, Tag>();
        CreateMap<Tag, TagResponse>();

        CreateMap<CreateFavoriteRequest, Favorite>();
        CreateMap<Favorite, FavoriteResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
            .ForMember(dest => dest.SubjectName,
                src => src.MapFrom(x => x.Subject.Name));

        CreateMap<CreateTagSubjectRequest, TagSubject>();
        CreateMap<TagSubject, TagSubjectResponse>()
            .ForMember(dest => dest.TagName,
                src => src.MapFrom(x => x.Tag.Name))
            .ForMember(dest => dest.SubjectName,
                src => src.MapFrom(x => x.Subject.Name));
    }
}