using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class RoleQueryHandler :
    IRequestHandler<GetAllRoleQuery, ApiResponse<List<RoleResponse>>>,
    IRequestHandler<GetRoleByIdQuery, ApiResponse<RoleResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public RoleQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Role sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<RoleResponse>>> Handle(GetAllRoleQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Role>().Where(x=> x.IsActive == true).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<RoleResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Role>, List<RoleResponse>>(list);
         return new ApiResponse<List<RoleResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Role deðerlerinin alýndýðý query
    public async Task<ApiResponse<RoleResponse>> Handle(GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Role>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<RoleResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Role, RoleResponse>(entity);
        return new ApiResponse<RoleResponse>(mapped);
    }

}