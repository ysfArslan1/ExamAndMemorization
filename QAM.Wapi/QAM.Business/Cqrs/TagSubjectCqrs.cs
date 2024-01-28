using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateTagSubjectCommand(int CurrentUserId, CreateTagSubjectRequest Model) : IRequest<ApiResponse<TagSubjectResponse>>;
public record UpdateTagSubjectCommand(int Id, int CurrentUserId, UpdateTagSubjectRequest Model) : IRequest<ApiResponse>;
public record DeleteTagSubjectCommand(int Id) : IRequest<ApiResponse>;

public record GetAllTagSubjectQuery() : IRequest<ApiResponse<List<TagSubjectResponse>>>;
public record GetTagSubjectByIdQuery(int Id) : IRequest<ApiResponse<TagSubjectResponse>>;

