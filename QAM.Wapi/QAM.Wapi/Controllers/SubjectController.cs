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
public class SubjectController : ControllerBase
{
    private readonly IMediator mediator;
    public SubjectController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<SubjectResponse>>> Get()
    {
        var operation = new GetAllSubjectQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Subject verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<SubjectResponse>> Get(int id)
    {
        var operation = new GetSubjectByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Subject verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<SubjectResponse>> Post([FromBody] CreateSubjectRequest Subject)
    {
        // Validation iþlemi uygulanýr
        CreateSubjectRequestValidator validator = new CreateSubjectRequestValidator();
        validator.ValidateAndThrow(Subject);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);
        CurrentUserId = 1;

        var operation = new CreateSubjectCommand(CurrentUserId, Subject);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Subject verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateSubjectRequest Subject)
    {
        // Validation iþlemi uygulanýr
        UpdateSubjectRequestValidator validator = new UpdateSubjectRequestValidator();
        validator.ValidateAndThrow(Subject);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);
        CurrentUserId = 1;

        var operation = new UpdateSubjectCommand(id, CurrentUserId, Subject);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Subject verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteSubjectCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}