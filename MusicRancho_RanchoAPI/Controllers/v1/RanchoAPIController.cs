using AutoMapper;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Models.Dto;
using MusicRancho_RanchoAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MusicRancho_RanchoAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/RanchoAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RanchoAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IRanchoRepository _dbRancho;
        private readonly IMapper _mapper;
        public RanchoAPIController(IRanchoRepository dbRancho, IMapper mapper)
        {
            _dbRancho = dbRancho;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRanchos([FromQuery(Name = "filterOccupancy")] int? occupancy,
            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<Rancho> ranchoList;

                if (occupancy > 0)
                {
                    ranchoList = await _dbRancho.GetAllAsync(u => u.Occupancy == occupancy, pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    ranchoList = await _dbRancho.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    ranchoList = ranchoList.Where(u => u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<RanchoDTO>>(ranchoList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetRancho")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(RanchoDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetRancho(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var rancho = await _dbRancho.GetAsync(u => u.Id == id);
                if (rancho == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<RanchoDTO>(rancho);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRancho([FromBody] RanchoCreateDTO createDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _dbRancho.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Rancho already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                //if (ranchoDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                Rancho rancho = _mapper.Map<Rancho>(createDTO);

                //Rancho model = new()
                //{
                //    Amenity = createDTO.Amenity,
                //    Details = createDTO.Details,
                //    ImageUrl = createDTO.ImageUrl,
                //    Name = createDTO.Name,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft
                //};
                await _dbRancho.CreateAsync(rancho);
                _response.Result = _mapper.Map<RanchoDTO>(rancho);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetRancho", new { id = rancho.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteRancho")]
        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        public async Task<ActionResult<APIResponse>> DeleteRancho(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var rancho = await _dbRancho.GetAsync(u => u.Id == id);
                if (rancho == null)
                {
                    return NotFound();
                }
                await _dbRancho.RemoveAsync(rancho);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        
        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [HttpPut("{id:int}", Name = "UpdateRancho")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRancho(int id, [FromBody] RanchoUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Rancho model = _mapper.Map<Rancho>(updateDTO);

                await _dbRancho.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialRancho")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialRancho(int id, JsonPatchDocument<RanchoUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var rancho = await _dbRancho.GetAsync(u => u.Id == id, tracked: false);

            RanchoUpdateDTO ranchoDTO = _mapper.Map<RanchoUpdateDTO>(rancho);


            if (rancho == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(ranchoDTO, ModelState);
            Rancho model = _mapper.Map<Rancho>(ranchoDTO);

            await _dbRancho.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}
