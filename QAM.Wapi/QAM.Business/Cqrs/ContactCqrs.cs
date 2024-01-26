using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateContactCommand(int CurrentUserId, CreateContactRequest Model) : IRequest<ApiResponse<ContactResponse>>;
public record UpdateContactCommand(int Id, int CurrentUserId, UpdateContactRequest Model) : IRequest<ApiResponse>;
public record DeleteContactCommand(int Id) : IRequest<ApiResponse>;

public record GetAllContactQuery() : IRequest<ApiResponse<List<ContactResponse>>>;
public record GetContactByIdQuery(int Id) : IRequest<ApiResponse<ContactResponse>>;

