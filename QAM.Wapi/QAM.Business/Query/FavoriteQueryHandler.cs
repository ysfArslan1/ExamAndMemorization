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

    // Favorite s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<FavoriteResponse>>> Handle(GetAllFavoriteQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Favorite>().Include(x=>x.User)
            .Include(x=>x.Subject).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<FavoriteResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Favorite>, List<FavoriteResponse>>(list);
         return new ApiResponse<List<FavoriteResponse>>(mappedList);
    }

    // �d de�eri ile istenilen Favorite de�erlerinin al�nd��� query
    public async Task<ApiResponse<FavoriteResponse>> Handle(GetFavoriteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Favorite>().Include(x => x.User)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<FavoriteResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Favorite, FavoriteResponse>(entity);
        return new ApiResponse<FavoriteResponse>(mapped);
    }

}