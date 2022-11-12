using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicRancho_Web.Models;
using MusicRancho_Web.Models.Dto;
using MusicRancho_Web.Models.VM;
using MusicRancho_Web.Services.IServices;
using Newtonsoft.Json;
using System.Data;

namespace MusicRancho_Web.Controllers
{
    public class RanchoNumberController : Controller
    {
        private readonly IRanchoNumberService _ranchoNumberService;
        private readonly IRanchoService _ranchoService;
        private readonly IMapper _mapper;
        public RanchoNumberController(IRanchoNumberService ranchoNumberService, IMapper mapper, IRanchoService ranchoService)
        {
            _ranchoNumberService = ranchoNumberService;
            _mapper = mapper;
            _ranchoService = ranchoService;
        }

        public async Task<IActionResult> IndexRanchoNumber()
        {
            List<RanchoNumberDTO> list = new();

            var response = await _ranchoNumberService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RanchoNumberDTO>>(Convert.ToString(response.Result));
}
            return View(list);
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        public async Task<IActionResult> CreateRanchoNumber()
        {
            RanchoNumberCreateVM ranchoNumberVM = new();
            var response = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
{
                ranchoNumberVM.RanchoList = JsonConvert.DeserializeObject<List<RanchoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(ranchoNumberVM);
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRanchoNumber(RanchoNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _ranchoNumberService.CreateAsync<APIResponse>(model.RanchoNumber, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexRanchoNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (resp != null && resp.IsSuccess)
            {
                model.RanchoList = JsonConvert.DeserializeObject<List<RanchoDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        public async Task<IActionResult> UpdateRanchoNumber(int ranchoNo)
        {
            RanchoNumberUpdateVM ranchoNumberVM = new();
            var response = await _ranchoNumberService.GetAsync<APIResponse>(ranchoNo, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                RanchoNumberDTO model = JsonConvert.DeserializeObject<RanchoNumberDTO>(Convert.ToString(response.Result));
                ranchoNumberVM.RanchoNumber =  _mapper.Map<RanchoNumberUpdateDTO>(model);
            }

            response = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                ranchoNumberVM.RanchoList = JsonConvert.DeserializeObject<List<RanchoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); 
                return View(ranchoNumberVM);
            }


            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRanchoNumber(RanchoNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _ranchoNumberService.UpdateAsync<APIResponse>(model.RanchoNumber, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexRanchoNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (resp != null && resp.IsSuccess)
            {
                model.RanchoList = JsonConvert.DeserializeObject<List<RanchoDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        public async Task<IActionResult> DeleteRanchoNumber(int ranchoNo)
        {
            RanchoNumberDeleteVM ranchoNumberVM = new();
            var response = await _ranchoNumberService.GetAsync<APIResponse>(ranchoNo, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                RanchoNumberDTO model = JsonConvert.DeserializeObject<RanchoNumberDTO>(Convert.ToString(response.Result));
                ranchoNumberVM.RanchoNumber = model;
            }

            response = await _ranchoService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                ranchoNumberVM.RanchoList = JsonConvert.DeserializeObject<List<RanchoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(ranchoNumberVM);
            }


            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRanchoNumber(RanchoNumberDeleteVM model)
        {

            var response = await _ranchoNumberService.DeleteAsync<APIResponse>(model.RanchoNumber.RanchoNo, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexRanchoNumber));
            }

            return View(model);
        }
    }
}
