using Domain.Exceptions;

namespace Domain.Entities
{
    public class GameClubEvent:AuditableEntity
    {
        public GameClubEvent()
        {
            
        }
        public GameClubEvent(
            string ClubId,
            string Title,
            DateTime ScheduledAt,
            string? Description
            ) {
            if (string.IsNullOrEmpty(Title))
            {
                throw new DomainArgumentException(nameof(Title));
            }
            this.Id = Guid.NewGuid().ToString();
            this.ClubId = ClubId;
            this.Title = Title;
            this.Description = Description;
            this.ScheduledAt = ScheduledAt;
            this.CreatedAt = DateTime.UtcNow;
        }
        public string Id { get;private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        //
        public string ClubId { get; private set; }
        //
        public GameClub Club { get; private set; } = null!;
        public void Update(
            string Title,
            DateTime ScheduledAt,
            string? Description

            )
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new DomainArgumentException(nameof(Title));
            }
            this.Title = Title;
            this.Description = Description;
            this.ScheduledAt = ScheduledAt;
            this.UpdatedAt = DateTime.UtcNow;
        }

    }
}
