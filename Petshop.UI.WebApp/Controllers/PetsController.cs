using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationServices;
using Petshop.Core.Entity;
using Microsoft.Extensions.DependencyInjection;
using Petshop.Infrastructure.Data;


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
        public IEnumerable<Pet> Get()
        {
            return _petService.ReadAll();
        }

        // GET api/pets/5
        [HttpGet("{id}")]
        public Pet Get(int id)
        {
            return _petService.Read(id);
        }

        // POST api/pets
        [HttpPost]
        public Pet Post([FromBody] Pet pet)
        {
            Pet petCreated = _petService.Create(pet);
            return petCreated;
        }

        // PUT api/pets/5
        [HttpPut("{id}")]
        public Pet Put([FromBody] Pet pet)
        {
            // update method us not currently using an id
            Pet petUpdated = _petService.Update(pet);
            return petUpdated;
        }

        // DELETE api/pets/5
        [HttpDelete("{id}")]
        public Pet Delete(int id)
        {
            Pet petDeleted = _petService.Delete(id);
            return petDeleted;
        }
    }
}
