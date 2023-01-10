using GuClient.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GuClient.Controllers
{
    public class ContactController : Controller
    {
        private readonly IOptions<SpecCatalog> _speccatalog;

        public ContactController(IOptions<SpecCatalog> speccatalog)
        {
            _speccatalog = speccatalog;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
