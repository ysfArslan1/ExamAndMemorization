using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class TagQueryHandler :
    IRequestHandler<GetAllTagQuery, ApiResponse<List<TagResponse>>>,
    IRequestHandler<GetTagByIdQuery, ApiResponse<TagResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public TagQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Tag sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<TagResponse>>> Handle(GetAllTagQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Tag>().Where(x=> x.IsActive == true).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<TagResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Tag>, List<TagResponse>>(list);
         return new ApiResponse<List<TagResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Tag deðerlerinin alýndýðý query
    public async Task<ApiResponse<TagResponse>> Handle(GetTagByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Tag>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<TagResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Tag, TagResponse>(entity);
        return new ApiResponse<TagResponse>(mapped);
    }

}