using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Commands;
 public class UpdateGameClubEventCommand : IRequest
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public DateTime ScheduledAt { get; init; }
}
public class UpdateGameClubEventCommandHandler : IRequestHandler<UpdateGameClubEventCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateGameClubEventCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateGameClubEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GameClubEvent
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        entity.Update(request.Title,request.ScheduledAt, request.Description);
        await _context.SaveChangesAsync(cancellationToken);
    }
}