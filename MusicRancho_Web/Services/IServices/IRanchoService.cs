using MusicRancho_Web.Models.Dto;

namespace MusicRancho_Web.Services.IServices
{
    public interface IRanchoService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(RanchoCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(RanchoUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
