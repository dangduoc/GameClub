using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Commands;
public class DeleteGameClubEventCommand : IRequest
{
    public string Id { get; set; }
}
public class DeleteGameClubEventCommandHandler : IRequestHandler<DeleteGameClubEventCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteGameClubEventCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteGameClubEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GameClubEvent
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        _context.GameClubEvent.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}

