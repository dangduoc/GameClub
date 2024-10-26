using Application.Common.Interfaces;
using Application.Common.Models;
namespace Application.Queries;

public class GetAllGameClubEventQuery : PaginationRequest, IRequest<PaginationResponse<GameClubEventDto>>
{
    public string ClubId { get; set; } = "";
}


public class GetAllGameClubEventQueryHandler : IRequestHandler<GetAllGameClubEventQuery, PaginationResponse<GameClubEventDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAllGameClubEventQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginationResponse<GameClubEventDto>> Handle(GetAllGameClubEventQuery request, CancellationToken cancellationToken)
    {
        var query = _context.GameClubEvent.AsNoTracking()
            .Where(x=>x.ClubId == request.ClubId);

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = request.SearchBy switch
            {
                "title" => query = query.Where(x => x.Title.ToLower().Contains(request.SearchText.ToLower().Trim())),
                _ => query = query.Where(x => x.Title.ToLower().Contains(request.SearchText.ToLower().Trim())),
            };
        }

        query = request.SortBy switch
        {
            "title" => query.OrderBy(x => x.Title),
            _ => query.OrderBy(x => x.CreatedAt),
        };
        if (request.IsSortDesc)
        {
            query = query.Reverse();
        }
        return await PaginationResponse<GameClubEventDto>.
            FromQuery(query,
            (x) => new GameClubEventDto(x),
            request.Start,
            request.Limit);
    }
}