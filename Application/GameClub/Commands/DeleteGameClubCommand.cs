using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Commands;
 public class DeleteGameClubCommand : IRequest
{
    public string Id { get; set; }
}
public class DeleteGameClubCommandHandler : IRequestHandler<DeleteGameClubCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteGameClubCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteGameClubCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GameClub
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        _context.GameClub.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}