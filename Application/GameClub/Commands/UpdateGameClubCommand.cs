using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Commands;
 public class UpdateGameClubCommand : IRequest
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
}

public class UpdateGameClubCommandValidator : AbstractValidator<UpdateGameClubCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateGameClubCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(500)
            .MustAsync((x, cancellation) => BeUniqueName(x, cancellation))
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueName(string Name, CancellationToken cancellationToken)
    {
        return await _context.GameClub
            .AllAsync(x => x.Name.ToLower()
            != Name.ToLower(),
            cancellationToken);
    }
}
public class UpdateGameClubCommandHandler : IRequestHandler<UpdateGameClubCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateGameClubCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateGameClubCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.GameClub
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        entity.Update(request.Name, request.Description);
        await _context.SaveChangesAsync(cancellationToken);
    }
}