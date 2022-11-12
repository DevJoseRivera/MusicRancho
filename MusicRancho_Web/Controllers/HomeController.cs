using AutoMapper;
using MusicRancho_Utility;
using MusicRancho_Web.Models;
using MusicRancho_Web.Models.Dto;
using MusicRancho_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicRancho_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRanchoService _ranchoService;
        private readonly IMapper _mapper;
        public HomeController(IRanchoService ranchoService, IMapper mapper)
        {
            _ranchoService = ranchoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<RanchoDTO> list = new();

            var response = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RanchoDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
       
    }
}