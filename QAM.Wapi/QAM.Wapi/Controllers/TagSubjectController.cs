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
public class TagSubjectController : ControllerBase
{
    private readonly IMediator mediator;
    public TagSubjectController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<TagSubjectResponse>>> Get()
    {
        var operation = new GetAllTagSubjectQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan TagSubject verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    public async Task<ApiResponse<TagSubjectResponse>> Get(int id)
    {
        var operation = new GetTagSubjectByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de TagSubject verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] CreateTagSubjectRequest TagSubject)
    {
        // Validation i�lemi uygulan�r
        CreateTagSubjectRequestValidator validator = new CreateTagSubjectRequestValidator();
        validator.ValidateAndThrow(TagSubject);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new CreateTagSubjectCommand(CurrentUserId, TagSubject);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen TagSubject verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateTagSubjectRequest TagSubject)
    {
        // Validation i�lemi uygulan�r
        UpdateTagSubjectRequestValidator validator = new UpdateTagSubjectRequestValidator();
        validator.ValidateAndThrow(TagSubject);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new UpdateTagSubjectCommand(id, CurrentUserId, TagSubject);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen TagSubject verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteTagSubjectCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}