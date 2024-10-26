using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Commands;
public class CreateGameClubEventCommand : IRequest<GameClubEventDto>
{
    public string Title { get; init; }
    public string? Description { get; init; }
    public DateTime ScheduledAt { get; init; }
    public string ClubId { get; set; }
}
public class CreateGameClubEventCommandValidator : AbstractValidator<CreateGameClubEventCommand>
{
    public CreateGameClubEventCommandValidator()
    {
        RuleFor(v => v.ClubId).NotEmpty();
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(v => v.ScheduledAt)
        .NotNull();
    }

  
}

public class CreateGameClubEventCommandHandler : IRequestHandler<CreateGameClubEventCommand, GameClubEventDto>
{
    private readonly IApplicationDbContext _context;
    public CreateGameClubEventCommandHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }
    public async Task<GameClubEventDto> Handle(CreateGameClubEventCommand request, CancellationToken cancellationToken)
    {
        GameClubEvent entity = new GameClubEvent(request.ClubId,request.Title,request.ScheduledAt,request.Description);
        _context.GameClubEvent.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new GameClubEventDto(entity);
    }
}
