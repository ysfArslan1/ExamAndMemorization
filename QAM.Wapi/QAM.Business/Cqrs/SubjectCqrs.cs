using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateSubjectCommand(int CurrentUserId, CreateSubjectRequest Model) : IRequest<ApiResponse<SubjectResponse>>;
public record UpdateSubjectCommand(int Id, int CurrentUserId, UpdateSubjectRequest Model) : IRequest<ApiResponse>;
public record DeleteSubjectCommand(int Id) : IRequest<ApiResponse>;

public record GetAllSubjectQuery() : IRequest<ApiResponse<List<SubjectResponse>>>;
public record GetSubjectByIdQuery(int Id) : IRequest<ApiResponse<SubjectResponse>>;

