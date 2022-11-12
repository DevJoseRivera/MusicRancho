using MusicRancho_Utility;
using MusicRancho_Web.Models;
using MusicRancho_Web.Models.Dto;
using MusicRancho_Web.Services.IServices;

namespace MusicRancho_Web.Services
{
    public class RanchoService : BaseService, IRanchoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string ranchoUrl;

        public RanchoService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            ranchoUrl = configuration.GetValue<string>("ServiceUrls:RanchoAPI");

        }

        public Task<T> CreateAsync<T>(RanchoCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = ranchoUrl + "/api/v1/ranchoAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = ranchoUrl + "/api/v1/ranchoAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = ranchoUrl + "/api/v1/ranchoAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = ranchoUrl + "/api/v1/ranchoAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(RanchoUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = ranchoUrl + "/api/v1/ranchoAPI/" + dto.Id,
                Token = token
            }) ;
        }
    }
}
