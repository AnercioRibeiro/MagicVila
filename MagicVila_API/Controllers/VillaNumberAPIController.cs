using System;
using System.Net;
using AutoMapper;
using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VilaAPI.Controllers
{

        [Route("api/VillaNumberAPI")]
        [ApiController]
        public class VillaNumberAPIController : ControllerBase
        {
            protected APIResponse _response;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public VillaNumberAPIController(IUnitOfWork unitOfWork, IMapper mapper)
            {
            _unitOfWork = unitOfWork;
                _mapper = mapper;
                this._response = new();
               
            }


            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<APIResponse>> GetVillaNumbers()
            {
                try
                {

                    IEnumerable<VillaNumber> villaNumberList = await _unitOfWork.VillaNumberRepository.GetAllAsync(includeProperties: "Villa");
                    _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
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

            [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<APIResponse>> GetVillaNumber(int villaNo)
            {
                try
                {
                    if (villaNo == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }
                    var villaNumber = await _unitOfWork.VillaNumberRepository.GetAsync(u => u.VillaNo == villaNo);
                    if (villaNumber == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }
                    _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
            {
                try
                {

                    if (await _unitOfWork.VillaNumberRepository.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
                    {
                        ModelState.AddModelError("ErrorMessages", "Villa Number already Exists!");
                        return BadRequest(ModelState);
                    }
                    if (await _unitOfWork.VillaRepository.GetAsync(u => u.Id == createDTO.VillaID) == null)
                    {
                        ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                        return BadRequest(ModelState);
                    }
                    if (createDTO == null)
                    {
                        return BadRequest(createDTO);
                    }

                    VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);


                    await _unitOfWork.VillaNumberRepository.CreateAsync(villaNumber);
                    _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetVilla", new { id = villaNumber.VillaNo }, _response);
                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { ex.ToString() };
                }
                return _response;
            }

            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
            public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNo)
            {
                try
                {
                    if (villaNo == 0)
                    {
                        return BadRequest();
                    }
                    var villaNumber = await _unitOfWork.VillaNumberRepository.GetAsync(u => u.VillaNo == villaNo);
                    if (villaNumber == null)
                    {
                        return NotFound();
                    }
                    await _unitOfWork.VillaNumberRepository.RemoveAsync(villaNumber);
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

            [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int villaNo, [FromBody] VillaNumberUpdateDTO updateDTO)
            {
                try
                {
                    if (updateDTO == null || villaNo != updateDTO.VillaNo)
                    {
                        return BadRequest();
                    }
                    if (await _unitOfWork.VillaRepository.GetAsync(u => u.Id == updateDTO.VillaID) == null)
                    {
                        ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                        return BadRequest(ModelState);
                    }
                    VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

                    await _unitOfWork.VillaNumberRepository.UpdateAsync(model);
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
