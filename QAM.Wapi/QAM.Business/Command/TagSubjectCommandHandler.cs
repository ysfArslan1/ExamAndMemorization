using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class TagSubjectCommandHandler :
    IRequestHandler<CreateTagSubjectCommand, ApiResponse<TagSubjectResponse>>,
    IRequestHandler<UpdateTagSubjectCommand,ApiResponse>,
    IRequestHandler<DeleteTagSubjectCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public TagSubjectCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // TagSubject sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<TagSubjectResponse>> Handle(CreateTagSubjectCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<TagSubject>().Where(x => x.TagId == request.Model.TagId && x.SubjectId == request.Model.SubjectId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<TagSubjectResponse>($"{request.Model.TagId} Tag and {request.Model.SubjectId} Subject  used by another TagSubject.");
        }

        var entity = mapper.Map<CreateTagSubjectRequest, TagSubject>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<TagSubject, TagSubjectResponse>(entityResult.Entity);
        return new ApiResponse<TagSubjectResponse>(mapped);
    }

    // TagSubject sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateTagSubjectCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<TagSubject>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }



        fromdb.TagId = request.Model.TagId;
        fromdb.SubjectId = request.Model.SubjectId;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // TagSubject sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteTagSubjectCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<TagSubject>().Where(x => x.Id == request.Id)
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