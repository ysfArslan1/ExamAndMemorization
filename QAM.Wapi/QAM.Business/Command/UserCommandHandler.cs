using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse>,
    IRequestHandler<UpdateUserCommand,ApiResponse>,
    IRequestHandler<DeleteUserCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // User sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<User>().Where(x => x.IdentityNumber == request.Model.IdentityNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.IdentityNumber} is used by another User.");
        }
        var checkRole = await dbContext.Set<Role>().Where(x => x.Id == request.Model.RoleId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkRole == null)
        {
            return new ApiResponse("Role not found");
        }

        var checkEmail = await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmail != null)
        {
            return new ApiResponse($"{request.Model.Email} is used by another User.");
        }

        var entity = mapper.Map<CreateUserRequest, User>(request.Model);
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }

    // User sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var checkRole = await dbContext.Set<Role>().Where(x => x.Id == request.Model.RoleId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkRole == null)
        {
            return new ApiResponse("Role not found");
        }
        var checkEmail = await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmail != null)
        {
            return new ApiResponse($"{request.Model.Email} is used by another User.");
        }


        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.RoleId = request.Model.RoleId;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // User sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete iþlemi yapýlýr
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}