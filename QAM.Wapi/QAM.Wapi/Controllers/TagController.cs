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
public class TagController : ControllerBase
{
    private readonly IMediator mediator;
    public TagController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<TagResponse>>> Get()
    {
        var operation = new GetAllTagQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Tag verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<TagResponse>> Get(int id)
    {
        var operation = new GetTagByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Tag verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] CreateTagRequest Tag)
    {
        // Validation iþlemi uygulanýr
        CreateTagRequestValidator validator = new CreateTagRequestValidator();
        validator.ValidateAndThrow(Tag);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new CreateTagCommand(CurrentUserId, Tag);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Tag verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateTagRequest Tag)
    {
        // Validation iþlemi uygulanýr
        UpdateTagRequestValidator validator = new UpdateTagRequestValidator();
        validator.ValidateAndThrow(Tag);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new UpdateTagCommand(id, CurrentUserId, Tag);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Tag verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteTagCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}