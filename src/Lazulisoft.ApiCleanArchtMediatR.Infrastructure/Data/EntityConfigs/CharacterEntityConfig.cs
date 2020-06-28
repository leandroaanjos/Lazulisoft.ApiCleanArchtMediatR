using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data.EntityConfigs
{
    public class CharacterEntityConfig : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Character");

            // Character class properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.FullName).HasColumnType("nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Homeworld).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.Species).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.Gender).IsRequired().HasColumnType("tinyint");
            builder.Property(x => x.Occupation).HasColumnType("nvarchar(100)").HasMaxLength(100);

            // BaseEntity class properties
            builder.Property(x => x.CreatedOn).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UpdatedOn).HasColumnType("datetime");
        }
    }
}
