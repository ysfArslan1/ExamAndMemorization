using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class ContactCommandHandler :
    IRequestHandler<CreateContactCommand, ApiResponse>,
    IRequestHandler<UpdateContactCommand,ApiResponse>,
    IRequestHandler<DeleteContactCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public ContactCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Contact s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email)
            .Include(x => x.User)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.Email} is used by another Contact.");
        }

        var entity = mapper.Map<CreateContactRequest, Contact>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }

    // Contact s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        fromdb.Email = request.Model.Email;
        fromdb.PhoneNumber = request.Model.PhoneNumber;
        fromdb.isDefault = request.Model.isDefault;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Contact s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete i�lemi yap�l�r
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}