using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questinator2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questinator2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepo;

        public UsersController(IUserRepository repo)
        {
            _userRepo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return new JsonResult( _userRepo.GetUsers());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _userRepo.GetUsers().FirstOrDefault(x => x.UserId == id);
            if (user == null)
                return NotFound();
            return new JsonResult(user);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userRepo.Create(user);
            
            return Ok(user);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_userRepo.GetUsers().Any(x => x.UserId == user.UserId))
            {
                return NotFound();
            }

            _userRepo.Update(user);
            
            return Ok(user);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            User user = _userRepo.GetUsers().FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepo.Delete(user.UserId);

            return Ok(user);
        }
    }
}
