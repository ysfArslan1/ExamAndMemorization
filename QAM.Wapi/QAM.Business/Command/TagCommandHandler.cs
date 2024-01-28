using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Command;

public class TagCommandHandler :
    IRequestHandler<CreateTagCommand, ApiResponse<TagResponse>>,
    IRequestHandler<UpdateTagCommand,ApiResponse>,
    IRequestHandler<DeleteTagCommand,ApiResponse>

{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public TagCommandHandler(QmDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Tag s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<TagResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Tag>().Where(x => x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<TagResponse>($"{request.Model.Name} is used by another Tag.");
        }

        var entity = mapper.Map<CreateTagRequest, Tag>(request.Model);
        entity.InsertDate = DateTime.Now;
        entity.InsertUserId =request.CurrentUserId;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Tag, TagResponse>(entityResult.Entity);
        return new ApiResponse<TagResponse>(mapped);
    }

    // Tag s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Tag>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        fromdb.Name = request.Model.Name;
        fromdb.Description = request.Model.Description;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Tag s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Tag>().Where(x => x.Id == request.Id)
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