using Application.Common.Interfaces;
using Application.Common.Models;
namespace Application.Queries;
public class GetAllGameClubQuery:PaginationRequest, IRequest<PaginationResponse<GameClubDto>>
{
    
}
public class GetAllGameClubQueryHandler : IRequestHandler<GetAllGameClubQuery, PaginationResponse<GameClubDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAllGameClubQueryHandler(IApplicationDbContext context)
    {
        _context = context; 
    }
    public async Task<PaginationResponse<GameClubDto>> Handle(GetAllGameClubQuery request, CancellationToken cancellationToken)
    {
        var query = _context.GameClub.AsNoTracking();

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = request.SearchBy switch
            {
                "name" => query = query.Where(x => x.Name.ToLower().Contains(request.SearchText.ToLower().Trim())),
                _ => query = query.Where(x => x.Name.ToLower().Contains(request.SearchText.ToLower().Trim())),
            };
        }

        query = request.SortBy switch
        {
            "name" => query.OrderBy(x => x.Name),
            _ => query.OrderBy(x => x.CreatedAt),
        };
        if (request.IsSortDesc)
        {
            query = query.Reverse();
        }
        return await PaginationResponse<GameClubDto>.
            FromQuery(query, 
            (x)=> new GameClubDto(x),
            request.Start, 
            request.Limit);
    }
}