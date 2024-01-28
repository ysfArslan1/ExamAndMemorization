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
public class ContactController : ControllerBase
{
    private readonly IMediator mediator;
    public ContactController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ContactResponse>>> Get()
    {
        var operation = new GetAllContactQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Contact verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    public async Task<ApiResponse<ContactResponse>> Get(int id)
    {
        var operation = new GetContactByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Contact verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    public async Task<ApiResponse<ContactResponse>> Post([FromBody] CreateContactRequest Contact)
    {
        // Validation i�lemi uygulan�r
        CreateContactRequestValidator validator = new CreateContactRequestValidator();
        validator.ValidateAndThrow(Contact);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new CreateContactCommand(CurrentUserId, Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateContactRequest Contact)
    {
        // Validation i�lemi uygulan�r
        UpdateContactRequestValidator validator = new UpdateContactRequestValidator();
        validator.ValidateAndThrow(Contact);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new UpdateContactCommand(id, CurrentUserId, Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteContactCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}