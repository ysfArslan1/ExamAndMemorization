using MediatR;
using Microsoft.AspNetCore.Mvc;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Schema;

namespace QAM.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

  
}