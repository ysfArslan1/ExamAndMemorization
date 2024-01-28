using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateTagCommand(int CurrentUserId, CreateTagRequest Model) : IRequest<ApiResponse<TagResponse>>;
public record UpdateTagCommand(int Id, int CurrentUserId, UpdateTagRequest Model) : IRequest<ApiResponse>;
public record DeleteTagCommand(int Id) : IRequest<ApiResponse>;

public record GetAllTagQuery() : IRequest<ApiResponse<List<TagResponse>>>;
public record GetTagByIdQuery(int Id) : IRequest<ApiResponse<TagResponse>>;

