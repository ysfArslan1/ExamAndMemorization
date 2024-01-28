using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class FavoriteQueryHandler :
    IRequestHandler<GetAllFavoriteQuery, ApiResponse<List<FavoriteResponse>>>,
    IRequestHandler<GetFavoriteByIdQuery, ApiResponse<FavoriteResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public FavoriteQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Favorite sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<FavoriteResponse>>> Handle(GetAllFavoriteQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Favorite>().Include(x=>x.User)
            .Include(x=>x.Subject).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<FavoriteResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Favorite>, List<FavoriteResponse>>(list);
         return new ApiResponse<List<FavoriteResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Favorite deðerlerinin alýndýðý query
    public async Task<ApiResponse<FavoriteResponse>> Handle(GetFavoriteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Favorite>().Include(x => x.User)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<FavoriteResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Favorite, FavoriteResponse>(entity);
        return new ApiResponse<FavoriteResponse>(mapped);
    }

}