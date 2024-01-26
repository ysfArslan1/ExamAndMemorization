using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class RoleCommandHandler :
    IRequestHandler<CreateRoleCommand, ApiResponse<RoleResponse>>,
    IRequestHandler<UpdateRoleCommand,ApiResponse>,
    IRequestHandler<DeleteRoleCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public RoleCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Role sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Role>().Where(x => x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<RoleResponse>($"{request.Model.Name} is used by another Role.");
        }

        var entity = mapper.Map<CreateRoleRequest, Role>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Role, RoleResponse>(entityResult.Entity);
        return new ApiResponse<RoleResponse>(mapped);
    }

    // Role sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Role>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }



        fromdb.Name = request.Model.Name;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Role sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Role>().Where(x => x.Id == request.Id)
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