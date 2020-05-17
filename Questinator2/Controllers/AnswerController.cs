using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questinator2.Models;



namespace Questinator2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private IAnswerRepository _userRepo;

        public AnswerController(IAnswerRepository repo)
        {
            _userRepo = repo;
        }
        // GET: api/Answer
        [HttpGet]
        public ActionResult<IEnumerable<Answer>> Get()
        {
            return new JsonResult(_userRepo.GetAnswers());
        }

        // GET: api/Answer/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Answer> Get(int id)
        {
            Answer answer = _userRepo.GetAnswers().FirstOrDefault(x => x.AnswerId == id);
            if (answer == null)
                return NotFound();
            return new JsonResult(answer);
        }

        // POST: api/Answer
        [HttpPost]
        public ActionResult<Answer> Post(Answer answer)
        {
            if (answer == null)
            {
                return BadRequest();
            }

            _userRepo.Create(answer);

            return Ok(answer);
        }

        // PUT: api/Answer/5
        [HttpPut("{id}")]
        public ActionResult<Answer> Put(Answer answer)
        {
            if (answer == null)
            {
                return BadRequest();
            }
            if (!_userRepo.GetAnswers().Any(x => x.AnswerId == answer.AnswerId))
            {
                return NotFound();
            }

            _userRepo.Update(answer);

            return Ok(answer);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Answer> Delete(int id)
        {
            Answer answer = _userRepo.GetAnswers().FirstOrDefault(x => x.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            _userRepo.Delete(answer.AnswerId);

            return Ok(answer);
        }
    }
}
