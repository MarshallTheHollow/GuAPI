using GuClient.Models.Config;
using GuData.DTO;
using GuData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace GuClient.Controllers
{
    public class GroupController : Controller
    {
        private readonly IOptions<SpecCatalog> _speccatalog;

        public GroupController(IOptions<SpecCatalog> speccatalog) 
        {
            _speccatalog = speccatalog;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await GetGroups();
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupDTO model)
        {
            bool result = await AddGroup(model);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeleteGroup(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var group = await GetGroup(id);
            return View(group);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupDTO model, int id)
        {
            bool result = await EditGroup(model, id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        private async Task<bool> EditGroup(GroupDTO model, int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsJsonAsync((string)_speccatalog.Value.Endpoint + "Group/" + id, model);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private async Task<bool> DeleteGroup(int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.DeleteAsync((string)_speccatalog.Value.Endpoint + "Group/" + id);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        private async Task<Group> GetGroup(int id)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync((string)_speccatalog.Value.Endpoint + "Group/" + id);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<Group>(responseData);
                    return result;
                }
            }
            catch { }
            return null;
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

        private async Task<bool> AddGroup(GroupDTO model)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PutAsJsonAsync((string)_speccatalog.Value.Endpoint + "Group", model);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
