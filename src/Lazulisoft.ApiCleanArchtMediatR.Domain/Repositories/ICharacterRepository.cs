using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories
{
    public interface ICharacterRepository : IRepository<Character>
    {
        Task<Character> GetById(int id);
    }
}