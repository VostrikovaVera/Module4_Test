using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4_Test.Entities;

namespace Module4_Test.EntityConfigurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist").HasKey(p => p.ArtistId);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateOfBirth).IsRequired().HasColumnType("date");
            builder.Property(p => p.Phone).IsRequired(false).HasMaxLength(13);
            builder.Property(p => p.Email).IsRequired(false);
            builder.Property(p => p.InstagramUrl).IsRequired(false);

            builder.HasMany(a => a.Songs)
                .WithMany(s => s.Artists)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistSong",
                    j => j
                        .HasOne<Song>()
                        .WithMany()
                        .HasForeignKey("SongId"),
                    j => j
                        .HasOne<Artist>()
                        .WithMany()
                        .HasForeignKey("ArtistId"));
        }
    }
}
