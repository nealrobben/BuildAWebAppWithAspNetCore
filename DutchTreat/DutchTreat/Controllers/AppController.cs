using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMail _mailService;

        public AppController(IMail mailService)
        {
            _mailService = mailService;
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
            }
            else
            {
                //Show error
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }
    }
}