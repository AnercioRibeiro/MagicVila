using System;
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
        public VillaAPIController(ApplicationDbContext db) 
        {
           _db = db;
        } 
        [HttpGet]
		public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
          
            return Ok(await _db.Villas.ToListAsync());
             
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
            return Ok(villa);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDTO)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomErrorForNameonVillaDTO","Villa already exists!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);

            }
            //if (villaDTO.Id>0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.ImageUrl,
            };
            
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id !=villaDTO.Id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.ImageUrl,
            };
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

            VillaUpdateDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Amenity = villa.Amenity,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                ImageUrl = villa.ImageUrl,
            };

            if (villa == null)
            {
                return BadRequest();
            }

            villaPatchDTO.ApplyTo(villaDTO, ModelState);

            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ImageUrl = villaDTO.ImageUrl,
            };

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            {

            }
            return NoContent();
        }
    }
}

