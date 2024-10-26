using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GameClubEventConfiguration : IEntityTypeConfiguration<GameClubEvent>
    {
        public void Configure(EntityTypeBuilder<GameClubEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasMaxLength(32).IsFixedLength()
                .ValueGeneratedNever();

            builder.Property(x => x.Title)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(500);

            builder.Property(x => x.ScheduledAt)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(false)
                .IsUnicode();

            builder.HasOne(x => x.Club).WithMany(x => x.Events)
                .HasForeignKey(x => x.ClubId)
                .IsRequired();

        }
    }
}
