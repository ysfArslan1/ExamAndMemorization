using MediatR;
using QAM.Base.Response;
using QAM.Scheme;

namespace QAM.Business.Cqrs;


public record CreateFavoriteCommand(int CurrentUserId, CreateFavoriteRequest Model) : IRequest<ApiResponse>;
public record UpdateFavoriteCommand(int Id, int CurrentUserId, UpdateFavoriteRequest Model) : IRequest<ApiResponse>;
public record DeleteFavoriteCommand(int Id) : IRequest<ApiResponse>;

public record GetAllFavoriteQuery() : IRequest<ApiResponse<List<FavoriteResponse>>>;
public record GetFavoriteByIdQuery(int Id) : IRequest<ApiResponse<FavoriteResponse>>;

