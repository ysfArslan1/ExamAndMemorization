using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class SubjectQueryHandler :
    IRequestHandler<GetAllSubjectQuery, ApiResponse<List<SubjectResponse>>>,
    IRequestHandler<GetSubjectByIdQuery, ApiResponse<SubjectResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public SubjectQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Subject s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<SubjectResponse>>> Handle(GetAllSubjectQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Subject>().Where(x=> x.IsActive == true).Include(x => x.User)
            .ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<SubjectResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Subject>, List<SubjectResponse>>(list);
         return new ApiResponse<List<SubjectResponse>>(mappedList);
    }

    // �d de�eri ile istenilen Subject de�erlerinin al�nd��� query
    public async Task<ApiResponse<SubjectResponse>> Handle(GetSubjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Subject>().Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<SubjectResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Subject, SubjectResponse>(entity);
        return new ApiResponse<SubjectResponse>(mapped);
    }

}