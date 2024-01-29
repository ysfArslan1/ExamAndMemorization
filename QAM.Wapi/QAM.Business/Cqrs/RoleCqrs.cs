using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateRoleCommand(int CurrentUserId, CreateRoleRequest Model) : IRequest<ApiResponse>;
public record UpdateRoleCommand(int Id, int CurrentUserId, UpdateRoleRequest Model) : IRequest<ApiResponse>;
public record DeleteRoleCommand(int Id) : IRequest<ApiResponse>;

public record GetAllRoleQuery() : IRequest<ApiResponse<List<RoleResponse>>>;
public record GetRoleByIdQuery(int Id) : IRequest<ApiResponse<RoleResponse>>;

