using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries;
public class GetGameClubByIdQuery:IRequest<GameClubDto>
{
    public string Id { get; set; } = "";
}
public class GetGameClubByIdQueryHandler : IRequestHandler<GetGameClubByIdQuery, GameClubDto>
{
    private readonly IApplicationDbContext _context;
    public GetGameClubByIdQueryHandler(IApplicationDbContext context)
    {
        _context= context;
    }
    public async Task<GameClubDto> Handle(GetGameClubByIdQuery request, CancellationToken cancellationToken)
    {
        GameClub? entity = await _context.GameClub.FindAsync(request.Id);
        if (entity == null) {
            throw new NotFoundException();
        }
        return new GameClubDto(entity);
    }
}