using MusicRancho_RanchoAPI.Data;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MusicRancho_RanchoAPI.Repository
{
    public class RanchoRepository : Repository<Rancho>, IRanchoRepository
    {
        private readonly ApplicationDbContext _db;
        public RanchoRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

  
        public async Task<Rancho> UpdateAsync(Rancho entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Ranchos.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
