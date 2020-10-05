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
    public class UsersController : ControllerBase
    {
        // constructor for dependency injection
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<FilteredList<User>> Get()
        {
            try
            {
                var list = _userService.ReadAll();

                if (!list.Any()) return NoContent();
                return Ok(list);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try {
                return Ok(_userService.Read(id));
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

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                User userCreated = _userService.Create(user);
                return Created($"User with id {userCreated.Id} was created.", userCreated);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            return StatusCode(500, "Method not avaliable");
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            {
                return StatusCode(500, "Method not avaliable");
            }
        }
    }
}
