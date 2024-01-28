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

    // TagSubject s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<TagSubjectResponse>>> Handle(GetAllTagSubjectQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<TagSubject>().Where(x=> x.IsActive == true).Include(x=>x.Tag)
            .Include(x => x.Subject).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<TagSubjectResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<TagSubject>, List<TagSubjectResponse>>(list);
         return new ApiResponse<List<TagSubjectResponse>>(mappedList);
    }

    // �d de�eri ile istenilen TagSubject de�erlerinin al�nd��� query
    public async Task<ApiResponse<TagSubjectResponse>> Handle(GetTagSubjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<TagSubject>().Include(x => x.Tag)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<TagSubjectResponse>("Record not found");
        }
        
        var mapped = mapper.Map<TagSubject, TagSubjectResponse>(entity);
        return new ApiResponse<TagSubjectResponse>(mapped);
    }

}