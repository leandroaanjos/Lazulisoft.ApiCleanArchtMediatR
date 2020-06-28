using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CharacterEntityConfig());
        }
    }
}
