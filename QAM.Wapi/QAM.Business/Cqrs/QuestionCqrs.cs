using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateQuestionCommand(int CurrentUserId, CreateQuestionRequest Model) : IRequest<ApiResponse<QuestionResponse>>;
public record UpdateQuestionCommand(int Id, int CurrentUserId, UpdateQuestionRequest Model) : IRequest<ApiResponse>;
public record DeleteQuestionCommand(int Id) : IRequest<ApiResponse>;

public record GetAllQuestionQuery() : IRequest<ApiResponse<List<QuestionResponse>>>;
public record GetQuestionByIdQuery(int Id) : IRequest<ApiResponse<QuestionResponse>>;

