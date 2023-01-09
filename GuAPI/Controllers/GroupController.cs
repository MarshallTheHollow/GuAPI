using GuAPI.DTO;
using GuAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private ApplicationContext _context;
        public GroupController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Group> Get()
        {
            return _context.Groups;
        }

        [HttpGet("{id}")]
        public Group Get(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null)
            {
                return group;
            }
            return null;
        }

        [HttpPut]
        public IActionResult Put(GroupDTO model)
        {
            if (model != null)
            {
                Group group = new Group() { GroupNumber = model.GroupName, Institute = model.Institute };
                _context.Groups.Add(group);
                _context.SaveChanges();

                return Ok();
            }          
            return BadRequest();
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, GroupDTO model)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null)
            {
                group.GroupNumber = model.GroupName;
                group.Institute = model.Institute;
                _context.Groups.Update(group);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
