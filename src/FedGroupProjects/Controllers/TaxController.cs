using Microsoft.AspNetCore.Mvc;
using FedGroupProjects.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FedGroupProjects.Controllers
{
    public class TaxController : Controller
    {
        // GET: /Tax/
        public IActionResult Index()
        {
            var taxPayer = new TaxPayer();

            return View(taxPayer);
        }

        // POST: /Tax/
        [HttpPost]
        public IActionResult Index(TaxPayer taxPayer)
        {
            ModelState.Clear();

            taxPayer.CalculateTax();

            return View(taxPayer);
        }
    }
}
