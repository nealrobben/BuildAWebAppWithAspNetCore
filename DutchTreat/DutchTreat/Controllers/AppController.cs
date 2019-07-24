using DutchTreat.Data;
using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMail _mailService;
        private readonly IDutchRepository _repository;

        public AppController(IMail mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            //var results = _repository.GetAllProducts();

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

        [Authorize]
        public IActionResult Shop()
        {
            return View();
        }
    }
}