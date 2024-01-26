using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Response;
using QAM.Business.Cqrs;
using QAM.Data.Entity;
using QAM.Scheme;
using QAM.Data.DBOperations;

namespace QAM.Business.Query;

public class ContactQueryHandler :
    IRequestHandler<GetAllContactQuery, ApiResponse<List<ContactResponse>>>,
    IRequestHandler<GetContactByIdQuery, ApiResponse<ContactResponse>>
{
    private readonly QmDbContext dbContext;
    private readonly IMapper mapper;

    public ContactQueryHandler(QmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Contact sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetAllContactQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Contact>().Where(x=> x.IsActive == true)
            .Include(x=>x.User)
            .ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ContactResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
         return new ApiResponse<List<ContactResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Contact deðerlerinin alýndýðý query
    public async Task<ApiResponse<ContactResponse>> Handle(GetContactByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Contact>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ContactResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Contact, ContactResponse>(entity);
        return new ApiResponse<ContactResponse>(mapped);
    }

}