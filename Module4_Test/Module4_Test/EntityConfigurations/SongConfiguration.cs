using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4_Test.Entities;

namespace Module4_Test.EntityConfigurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Song").HasKey(p => p.SongId);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Duration).IsRequired().HasColumnType("time");
            builder.Property(p => p.ReleasedDate).IsRequired().HasColumnType("date");

            builder.HasOne(d => d.Genre)
            .WithMany(p => p.Songs)
            .HasForeignKey(d => d.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
