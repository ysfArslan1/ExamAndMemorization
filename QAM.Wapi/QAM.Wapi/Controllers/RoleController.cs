using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Business.Validator;
using QAM.Data;
using QAM.Data.Entity;
using QAM.Scheme;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace QAM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator mediator;
    public RoleController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<RoleResponse>>> Get()
    {
        var operation = new GetAllRoleQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Role verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<RoleResponse>> Get(int id)
    {
        var operation = new GetRoleByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Role verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<RoleResponse>> Post([FromBody] CreateRoleRequest Role)
    {
        // Validation iþlemi uygulanýr
        CreateRoleRequestValidator validator = new CreateRoleRequestValidator();
        validator.ValidateAndThrow(Role);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentRoleId = int.Parse(_id);

        var operation = new CreateRoleCommand(CurrentRoleId, Role);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Role verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateRoleRequest Role)
    {
        // Validation iþlemi uygulanýr
        UpdateRoleRequestValidator validator = new UpdateRoleRequestValidator();
        validator.ValidateAndThrow(Role);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentRoleId = int.Parse(_id);

        var operation = new UpdateRoleCommand(id,CurrentRoleId, Role);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Role verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteRoleCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}