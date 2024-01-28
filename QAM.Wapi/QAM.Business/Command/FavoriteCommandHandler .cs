using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class FavoriteCommandHandler :
    IRequestHandler<CreateFavoriteCommand, ApiResponse<FavoriteResponse>>,
    IRequestHandler<UpdateFavoriteCommand,ApiResponse>,
    IRequestHandler<DeleteFavoriteCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public FavoriteCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Favorite sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<FavoriteResponse>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Favorite>().Where(x => x.UserId == request.Model.UserId && x.SubjectId == request.Model.SubjectId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<FavoriteResponse>
                ($"{request.Model.UserId} User and {request.Model.SubjectId} Subject used by another Favorite.");
        }

        var entity = mapper.Map<CreateFavoriteRequest, Favorite>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId = request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Favorite, FavoriteResponse>(entityResult.Entity);
        return new ApiResponse<FavoriteResponse>(mapped);
    }

    // Favorite sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Favorite>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var check = await dbContext.Set<Favorite>().Where(x => x.UserId == request.Model.UserId && x.SubjectId == request.Model.SubjectId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.UserId} User and {request.Model.SubjectId} Subject used by another Favorite.");
        }

        fromdb.UserId = request.Model.UserId;
        fromdb.SubjectId = request.Model.SubjectId;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Favorite sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Favorite>().Where(x => x.Id == request.Id)
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