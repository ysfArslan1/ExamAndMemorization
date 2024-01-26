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
public class QuestionController : ControllerBase
{
    private readonly IMediator mediator;
    public QuestionController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<QuestionResponse>>> Get()
    {
        var operation = new GetAllQuestionQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Question verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<QuestionResponse>> Get(int id)
    {
        var operation = new GetQuestionByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Question verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<QuestionResponse>> Post([FromBody] CreateQuestionRequest Question)
    {
        // Validation iþlemi uygulanýr
        CreateQuestionRequestValidator validator = new CreateQuestionRequestValidator();
        validator.ValidateAndThrow(Question);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentQuestionId = int.Parse(_id);

        var operation = new CreateQuestionCommand(CurrentQuestionId, Question);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Question verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateQuestionRequest Question)
    {
        // Validation iþlemi uygulanýr
        UpdateQuestionRequestValidator validator = new UpdateQuestionRequestValidator();
        validator.ValidateAndThrow(Question);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentQuestionId = int.Parse(_id);

        var operation = new UpdateQuestionCommand(id,CurrentQuestionId, Question);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Question verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteQuestionCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}