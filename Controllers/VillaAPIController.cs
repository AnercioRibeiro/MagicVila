﻿using System;
using AutoMapper;
using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VilaAPI.Controllers
{
    //[Route("api/[Controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
	public class VillaAPIController : ControllerBase
	{
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper) 
        {
           _db = db;
            _mapper = mapper;
        } 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
          
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
             
        }
        [HttpGet("id:int", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
              
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaCreateDTO)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaCreateDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomErrorForNameonVillaDTO","Villa already exists!");
                return BadRequest(ModelState);
            }
            if (villaCreateDTO == null)
            {
                return BadRequest(villaCreateDTO);

            }
            //if (villaDTO.Id>0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Villa model = _mapper.Map<Villa>(villaCreateDTO);
       
           await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u=>u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaDTO villaUpdateDTO)
        {
            if (villaUpdateDTO == null || id != villaUpdateDTO.Id)
            {
                return BadRequest();
            }
            Villa model = _mapper.Map<Villa>(villaUpdateDTO);
      
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> villaPatchDTO)
        {
            if (villaPatchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaUpdateDTO villaPartialDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            villaPatchDTO.ApplyTo(villaPartialDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaPartialDTO);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return NoContent();
        }
    }
}

