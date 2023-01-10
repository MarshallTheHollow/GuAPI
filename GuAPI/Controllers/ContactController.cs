using GuAPI.Models;
using GuData.DTO;
using GuData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ApplicationContext _context;
        public ContactController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _context.Contacts.Include(x => x.Group);
        }

        [HttpGet("{id}")]
        public Contact Get(int id)
        {
            var contact = _context.Contacts.Include(y => y.Group).FirstOrDefault(x => x.Id == id);
            if(contact != null)
            {
                return contact;
            }
            return null;
        }

        [HttpPut]
        public IActionResult Put(ContactDTO model)
        {
            if (model != null) 
            {
                Contact contact = new Contact() { Name = model.Name, PhoneNumber = model.PhoneNumber, SurName = model.SurName, GroupId = model.GroupId };
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                return Ok();
            }           
            return BadRequest();
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, ContactDTO model)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if(contact != null)
            {
                contact.Name = model.Name;
                contact.PhoneNumber = model.PhoneNumber;
                contact.SurName = model.SurName;
                contact.GroupId = model.GroupId;
                _context.Contacts.Update(contact);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();         
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
