using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Repositories
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual async Task<Character> GetById(int id)
        {
            return await base.Find(x => x.Id == id);
        }
    }
}