using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class TagSubjectQueryHandler :
    IRequestHandler<GetAllTagSubjectQuery, ApiResponse<List<TagSubjectResponse>>>,
    IRequestHandler<GetTagSubjectByIdQuery, ApiResponse<TagSubjectResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public TagSubjectQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // TagSubject sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<TagSubjectResponse>>> Handle(GetAllTagSubjectQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<TagSubject>().Where(x=> x.IsActive == true).Include(x=>x.Tag)
            .Include(x => x.Subject).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<TagSubjectResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<TagSubject>, List<TagSubjectResponse>>(list);
         return new ApiResponse<List<TagSubjectResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen TagSubject deðerlerinin alýndýðý query
    public async Task<ApiResponse<TagSubjectResponse>> Handle(GetTagSubjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<TagSubject>().Include(x => x.Tag)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<TagSubjectResponse>("Record not found");
        }
        
        var mapped = mapper.Map<TagSubject, TagSubjectResponse>(entity);
        return new ApiResponse<TagSubjectResponse>(mapped);
    }

}