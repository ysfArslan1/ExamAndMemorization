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
public class FavoriteController : ControllerBase
{
    private readonly IMediator mediator;
    public FavoriteController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<FavoriteResponse>>> Get()
    {
        var operation = new GetAllFavoriteQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Favorite verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<FavoriteResponse>> Get(int id)
    {
        var operation = new GetFavoriteByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Favorite verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<FavoriteResponse>> Post([FromBody] CreateFavoriteRequest Favorite)
    {
        // Validation iþlemi uygulanýr
        CreateFavoriteRequestValidator validator = new CreateFavoriteRequestValidator();
        validator.ValidateAndThrow(Favorite);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new CreateFavoriteCommand(CurrentUserId, Favorite);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Favorite verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateFavoriteRequest Favorite)
    {
        // Validation iþlemi uygulanýr
        UpdateFavoriteRequestValidator validator = new UpdateFavoriteRequestValidator();
        validator.ValidateAndThrow(Favorite);

        //string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        //int CurrentUserId = int.Parse(_id);
        int CurrentUserId = 1;

        var operation = new UpdateFavoriteCommand(id, CurrentUserId, Favorite);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Favorite verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteFavoriteCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

}