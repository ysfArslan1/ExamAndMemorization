using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class QuestionQueryHandler :
    IRequestHandler<GetAllQuestionQuery, ApiResponse<List<QuestionResponse>>>,
    IRequestHandler<GetQuestionByIdQuery, ApiResponse<QuestionResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public QuestionQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Question sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<QuestionResponse>>> Handle(GetAllQuestionQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Question>().Where(x=> x.IsActive == true).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<QuestionResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Question>, List<QuestionResponse>>(list);
         return new ApiResponse<List<QuestionResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Question deðerlerinin alýndýðý query
    public async Task<ApiResponse<QuestionResponse>> Handle(GetQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Question>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<QuestionResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Question, QuestionResponse>(entity);
        return new ApiResponse<QuestionResponse>(mapped);
    }

}