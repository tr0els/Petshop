using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Petshop.UI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        // constructor for dependency injection
        private readonly IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/pets
        [HttpGet]
        public ActionResult<FilteredList<Pet>> Get([FromQuery] Filter filter)
        {
            try
            {
                var filteredList = _petService.ReadAll(filter);

                // TODO: implement the usage of DTOs
                if (filteredList.TotalCount == 0) return NoContent();
                return Ok(filteredList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/pets/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try 
            {
                return Ok(_petService.Read(id));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message); // NoContent would be more correct?
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/pets
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
                Pet petCreated = _petService.Create(pet);
                return Created($"Pet with id {petCreated.Id} was created.", petCreated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/pets/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            // use id from url over any potential id from object
            pet.Id = id;

            try
            {
                Pet petUpdated = _petService.Update(pet);
                return Accepted($"Pet with id {petUpdated.Id} was updated.", petUpdated);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/pets/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                Pet petDeleted = _petService.Delete(id);
                return Accepted($"Pet with id {petDeleted.Id} was deleted.", petDeleted);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
