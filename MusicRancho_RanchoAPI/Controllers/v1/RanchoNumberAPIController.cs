using AutoMapper;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Models.Dto;
using MusicRancho_RanchoAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MusicRancho_RanchoAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/RanchoNumberAPI")]
    [ApiController]
    [ApiVersion("1.0")]

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


        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "string2" };
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Policy = "Employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRanchoNumbers()
        {
            try
            {

                IEnumerable<RanchoNumber> ranchoNumberList = await _dbRanchoNumber.GetAllAsync(includeProperties: "Rancho");
                _response.Result = _mapper.Map<List<RanchoNumberDTO>>(ranchoNumberList);
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


        [HttpGet("{id:int}", Name = "GetRanchoNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRanchoNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var ranchoNumber = await _dbRanchoNumber.GetAsync(u => u.RanchoNo == id);
                if (ranchoNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<RanchoNumberDTO>(ranchoNumber);
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

        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "Administrators")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRanchoNumber([FromBody] RanchoNumberCreateDTO createDTO)
        {
            try
            {

                if (await _dbRanchoNumber.GetAsync(u => u.RanchoNo == createDTO.RanchoNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Rancho Number already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _dbRancho.GetAsync(u => u.Id == createDTO.RanchoID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Rancho ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                RanchoNumber ranchoNumber = _mapper.Map<RanchoNumber>(createDTO);


                await _dbRanchoNumber.CreateAsync(ranchoNumber);
                _response.Result = _mapper.Map<RanchoNumberDTO>(ranchoNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetRancho", new { id = ranchoNumber.RanchoNo }, _response);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteRanchoNumber")]
        public async Task<ActionResult<APIResponse>> DeleteRanchoNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var ranchoNumber = await _dbRanchoNumber.GetAsync(u => u.RanchoNo == id);
                if (ranchoNumber == null)
                {
                    return NotFound();
                }
                await _dbRanchoNumber.RemoveAsync(ranchoNumber);
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
        [HttpPut("{id:int}", Name = "UpdateRanchoNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRanchoNumber(int id, [FromBody] RanchoNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.RanchoNo)
                {
                    return BadRequest();
                }
                if (await _dbRancho.GetAsync(u => u.Id == updateDTO.RanchoID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Rancho ID is Invalid!");
                    return BadRequest(ModelState);
                }
                RanchoNumber model = _mapper.Map<RanchoNumber>(updateDTO);

                await _dbRanchoNumber.UpdateAsync(model);
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


    }
}
