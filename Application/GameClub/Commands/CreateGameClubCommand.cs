using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Commands;
public class CreateGameClubCommand : IRequest<GameClubDto>
{
    public string Name { get; init; }
    public string? Description { get; init; }
}

public class CreateGameClubCommandValidator : AbstractValidator<CreateGameClubCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateGameClubCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(500);
    }
}

public class CreateGameClubCommandHandler : IRequestHandler<CreateGameClubCommand, GameClubDto>
{
    private readonly IApplicationDbContext _context;
    public CreateGameClubCommandHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<GameClubDto> Handle(CreateGameClubCommand request, CancellationToken cancellationToken)
    {
        if (await _context.GameClub.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()))
        {
            throw new AlreadyExisitedException("Name");
        }
        GameClub entity = new GameClub(request.Name, request.Description);
        _context.GameClub.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new GameClubDto(entity);
    }
}