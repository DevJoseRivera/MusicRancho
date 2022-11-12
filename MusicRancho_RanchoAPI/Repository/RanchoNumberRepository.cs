using MusicRancho_RanchoAPI.Data;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MusicRancho_RanchoAPI.Repository
{
    public class RanchoNumberRepository : Repository<RanchoNumber>, IRanchoNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public RanchoNumberRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

  
        public async Task<RanchoNumber> UpdateAsync(RanchoNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.RanchoNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
