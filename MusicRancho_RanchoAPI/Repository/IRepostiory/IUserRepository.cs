using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Models.Dto;

namespace MusicRancho_RanchoAPI.Repository.IRepostiory
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
