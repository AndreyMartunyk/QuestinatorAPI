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
    public class QuestionController : ControllerBase
    {
        private IQuestionRepository _repo;

        public QuestionController(IQuestionRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Question>> Get()
        {
            return new JsonResult(_repo.GetQuestions());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Question> Get(int id)
        {
            Question question = _repo.GetQuestions().FirstOrDefault(x => x.QuestionId == id);
            if (question == null)
                return NotFound();
            return new JsonResult(question);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Question> Post(Question question)
        {
            if (question == null)
            {
                return BadRequest();
            }

            _repo.Create(question);

            return Ok(question);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<Question> Put(Question question)
        {
            if (question == null)
            {
                return BadRequest();
            }
            if (!_repo.GetQuestions().Any(x => x.QuestionId == question.QuestionId))
            {
                return NotFound();
            }

            _repo.Update(question);

            return Ok(question);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<Question> Delete(int id)
        {
            Question question = _repo.GetQuestions().FirstOrDefault(x => x.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            _repo.Delete(question.QuestionId);

            return Ok(question);
        }
    }
}
