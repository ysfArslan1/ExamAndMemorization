using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class QuestionCommandHandler :
    IRequestHandler<CreateQuestionCommand, ApiResponse<QuestionResponse>>,
    IRequestHandler<UpdateQuestionCommand,ApiResponse>,
    IRequestHandler<DeleteQuestionCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public QuestionCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Question s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<QuestionResponse>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Question>().Where(x => x.question == request.Model.question)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<QuestionResponse>($"{request.Model.question} is used by another Question.");
        }

        var entity = mapper.Map<CreateQuestionRequest, Question>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Question, QuestionResponse>(entityResult.Entity);
        return new ApiResponse<QuestionResponse>(mapped);
    }

    // Question s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Question>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }



        fromdb.SubjectId = request.Model.SubjectId;
        fromdb.question = request.Model.question;
        fromdb.Explanation = request.Model.Explanation;
        fromdb.Status = request.Model.Status;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Question s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Question>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete i�lemi yap�l�r
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}