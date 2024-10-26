using Domain.Entities;

namespace Application.Common.Models;
 public class GameClubDto: AuditableEntityDto
{
    public GameClubDto(GameClub domain):base(domain)
    {
        Id = domain.Id;
        Name = domain.Name;
        Description = domain.Description;
    }
    public string Id { get;  set; }
    public string Name { get;  set; }
    public string? Description { get; set; }
}