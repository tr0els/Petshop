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
    public class OwnersController : ControllerBase
    {
        // constructor for dependency injection
        private readonly IOwnerService _ownerService;
        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/owners
        [HttpGet]
        public ActionResult<FilteredList<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                var filteredList = _ownerService.ReadAll(filter);
                var list = filteredList.List;

                // TODO: implement the usage of DTOs
                if (!filteredList.List.Any()) return NoContent();
                return Ok(filteredList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/owners/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try {
                return Ok(_ownerService.Read(id));
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

        // POST api/owners
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            try
            {
                Owner ownerCreated = _ownerService.Create(owner);
                return Created($"Owner with id {ownerCreated.Id} was created.", ownerCreated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/owners/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            // use id from url over any potential id from object
            owner.Id = id;

            try
            {
                Owner ownerUpdated = _ownerService.Update(owner);
                return Accepted($"Owner with id {ownerUpdated.Id} was updated.", ownerUpdated);
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

        // DELETE api/owners/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            try
            {
                Owner ownerDeleted = _ownerService.Delete(id);
                return Accepted($"Owner with id {ownerDeleted.Id} was deleted.", ownerDeleted);
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
