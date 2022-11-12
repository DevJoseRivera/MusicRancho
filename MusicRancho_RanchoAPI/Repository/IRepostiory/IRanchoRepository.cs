using MusicRancho_RanchoAPI.Models;
using System.Linq.Expressions;

namespace MusicRancho_RanchoAPI.Repository.IRepostiory
{
    public interface IRanchoRepository : IRepository<Rancho>
    {
      
        Task<Rancho> UpdateAsync(Rancho entity);
  
    }
}
