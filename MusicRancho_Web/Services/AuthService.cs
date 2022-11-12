using MusicRancho_Utility;
using MusicRancho_Web.Models;
using MusicRancho_Web.Models.Dto;
using MusicRancho_Web.Services.IServices;

namespace MusicRancho_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string ranchoUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            ranchoUrl = configuration.GetValue<string>("ServiceUrls:RanchoAPI");

        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = ranchoUrl + "/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = ranchoUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
