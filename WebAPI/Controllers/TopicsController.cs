using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicsController(TopicService topicService)
        {
            _topicService = topicService;
        }

        [EnableCors]
        [HttpGet]
        public ActionResult<List<Topic>> Get() =>
            _topicService.Get();

        [EnableCors("Paid")]
        [HttpGet("{id:length(24)}", Name = "GetTopic")]
        public ActionResult<Topic> Get(string id)
        {
            var topic = _topicService.Get(id);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        [HttpPost]
        public ActionResult<Topic> Create(Topic topic)
        {
            _topicService.Create(topic);

            return CreatedAtRoute("GetTopic", new { id = topic.TopicId.ToString() }, topic);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Topic topicIn)
        {
            var topic = _topicService.Get(id);

            if (topic == null)
            {
                return NotFound();
            }

            _topicService.Update(id, topicIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var topic = _topicService.Get(id);

            if (topic == null)
            {
                return NotFound();
            }

            _topicService.Remove(topic.TopicId);

            return NoContent();
        }
    }
}