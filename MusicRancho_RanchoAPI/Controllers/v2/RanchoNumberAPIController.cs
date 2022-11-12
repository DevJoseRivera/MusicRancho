using AutoMapper;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Mvc;

namespace MusicRancho_RanchoAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/RanchoNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class RanchoNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IRanchoNumberRepository _dbRanchoNumber;
        private readonly IRanchoRepository _dbRancho;
        private readonly IMapper _mapper;
        public RanchoNumberAPIController(IRanchoNumberRepository dbRanchoNumber, IMapper mapper,
            IRanchoRepository dbRancho)
        {
            _dbRanchoNumber = dbRanchoNumber;
            _mapper = mapper;
            _response = new();
            _dbRancho = dbRancho;
        }


        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Bhrugen", "DotNetMastery" };
        }


    }
}
