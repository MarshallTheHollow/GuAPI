using GuClient.Models.Config;
using GuData.DTO;
using GuData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace GuClient.Controllers
{
    public class ContactController : Controller
    {
        private readonly IOptions<SpecCatalog> _speccatalog;

        public ContactController(IOptions<SpecCatalog> speccatalog)
        {
            _speccatalog = speccatalog;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await GetContacts();
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Groups = await GetGroups();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactDTO model)
        {
            bool result = await AddContact(model);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeleteContact(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Groups = await GetGroups();
            var contact = await GetContact(id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactDTO model, int id)
        {
            bool result = await EditContact(model, id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        private async Task<bool> EditContact(ContactDTO model, int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsJsonAsync((string)_speccatalog.Value.Endpoint + "Contact/" + id, model);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private async Task<bool> DeleteContact(int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.DeleteAsync((string)_speccatalog.Value.Endpoint + "Contact/" + id);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private async Task<Contact> GetContact(int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync((string)_speccatalog.Value.Endpoint + "Contact/" + id);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<Contact>(responseData);
                    return result;
                }
            }
            catch { }
            return null;
        }

        private async Task<IEnumerable<Contact>> GetContacts()
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync((string)_speccatalog.Value.Endpoint + "Contact");
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responseData);
                    return result;
                }
            }
            catch { }
            return null;
        }

        private async Task<bool> AddContact(ContactDTO model)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PutAsJsonAsync((string)_speccatalog.Value.Endpoint + "Contact", model);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private async Task<IEnumerable<Group>> GetGroups()
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync((string)_speccatalog.Value.Endpoint + "Group");
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<IEnumerable<Group>>(responseData);
                    return result;
                }
            }
            catch { }
            return null;
        }
    }
}
