using MusicRancho_Web.Models.Dto;

namespace MusicRancho_Web.Services.IServices
{
    public interface IRanchoNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(RanchoNumberCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(RanchoNumberUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
