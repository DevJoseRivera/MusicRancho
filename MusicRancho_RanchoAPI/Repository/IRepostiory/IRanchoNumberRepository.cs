using MusicRancho_RanchoAPI.Models;
using System.Linq.Expressions;

namespace MusicRancho_RanchoAPI.Repository.IRepostiory
{
    public interface IRanchoNumberRepository : IRepository<RanchoNumber>
    {
      
        Task<RanchoNumber> UpdateAsync(RanchoNumber entity);
  
    }
}
