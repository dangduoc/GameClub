using Domain.Exceptions;

namespace Domain.Entities
{
    public class GameClub: AuditableEntity
    {
        public GameClub()
        {
            
        }
        public GameClub(string Name, string? Description)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new DomainArgumentException(nameof(Name));
            }
            this.Id = Guid.NewGuid().ToString();
            this.Name = Name;
            this.Description = Description;
            this.CreatedAt = DateTime.UtcNow;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public void Update(string Name, string? Description)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new DomainArgumentException(nameof(Name));
            }
            this.Name = Name;
            this.Description = Description;
            this.UpdatedAt = DateTime.UtcNow;
        }
        //
        public ICollection<GameClubEvent> Events { get; private set; } = new HashSet<GameClubEvent>();  
    }
}
