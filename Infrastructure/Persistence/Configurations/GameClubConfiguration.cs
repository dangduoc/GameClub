using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GameClubConfiguration : IEntityTypeConfiguration<GameClub>
    {
        public void Configure(EntityTypeBuilder<GameClub> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasMaxLength(32).IsFixedLength()
                .ValueGeneratedNever();
         

            builder.Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description)
                .IsRequired(false)
                .IsUnicode();


            builder.HasMany(x => x.Events).WithOne(x => x.Club)
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
