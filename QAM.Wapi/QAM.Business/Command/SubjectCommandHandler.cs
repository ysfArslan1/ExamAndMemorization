using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class SubjectCommandHandler :
    IRequestHandler<CreateSubjectCommand, ApiResponse>,
    IRequestHandler<UpdateSubjectCommand,ApiResponse>,
    IRequestHandler<DeleteSubjectCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public SubjectCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Subject sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Subject>().Where(x => x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.Name} is used by another Subject.");
        }

        var entity = mapper.Map<CreateSubjectRequest, Subject>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Subject, SubjectResponse>(entityResult.Entity);
        return new ApiResponse();
    }

    // Subject sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Subject>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        fromdb.Name = request.Model.Name;
        fromdb.Description = request.Model.Description;
        fromdb.isPublic = request.Model.isPublic;
        fromdb.LastActivityDate = request.Model.LastActivityDate;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Subject sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Subject>().Where(x => x.Id == request.Id)
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