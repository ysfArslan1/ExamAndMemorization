using MediatR;
using QAM.Base.Response;
using QAM.Schema;

namespace QAM.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;


