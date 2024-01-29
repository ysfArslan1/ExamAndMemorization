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

    // Database bulunan Tag verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    public async Task<ApiResponse<TagResponse>> Get(int id)
    {
        var operation = new GetTagByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Tag verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] CreateTagRequest Tag)
    {
        // Validation i�lemi uygulan�r
        CreateTagRequestValidator validator = new CreateTagRequestValidator();
        validator.ValidateAndThrow(Tag);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new CreateTagCommand(CurrentUserId, Tag);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Tag verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateTagRequest Tag)
    {
        // Validation i�lemi uygulan�r
        UpdateTagRequestValidator validator = new UpdateTagRequestValidator();
        validator.ValidateAndThrow(Tag);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new UpdateTagCommand(id, CurrentUserId, Tag);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Tag verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteTagCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}