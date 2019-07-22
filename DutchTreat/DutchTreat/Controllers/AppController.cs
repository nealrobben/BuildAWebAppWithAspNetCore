using System.Linq;
using DutchTreat.Data;
using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMail _mailService;
        private readonly DutchContext _context;

        public AppController(IMail mailService, DutchContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send mail
                _mailService.SendMessage("admin@site.com",model.Subject,model.Message);
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        public IActionResult Shop()
        {
            var results = _context.Products.OrderBy(x => x.Category).ToList();

            return View(results);
        }
    }
}