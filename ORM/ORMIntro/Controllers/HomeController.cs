using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORMIntro.Models;

namespace ORMIntro.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.AllSundaes = _context.Sundaes.ToList();

            return View();
        }

        [HttpGet("form")]
        public IActionResult SundaeForm()
        {
            return View();
        }

        [HttpPost("submit")]
        public IActionResult Submit(Sundae newSundae)
        {
            if(ModelState.IsValid)
            {
                _context.Sundaes.Add(newSundae);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("SundaeForm");            
        }

        [HttpGet("sundaes/{id}")]
        public IActionResult SingleSundae(int id)
        {
            Console.WriteLine(id);

            ViewBag.SingleSundae = _context.Sundaes
                .FirstOrDefault(sun => sun.SundaeId == id);
            return View();
        }

        [HttpGet("sundaes/{id}/delete")]
        public IActionResult DeleteSundae(int id)
        {
            Sundae deleteMe = _context.Sundaes
                .FirstOrDefault(sun => sun.SundaeId == id);

            _context.Sundaes.Remove(deleteMe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("sundaes/{id}/edit")]
        public IActionResult EditForm(int id)
        {
            Sundae displayMe = _context.Sundaes
                .FirstOrDefault(sun => sun.SundaeId == id);

            return View(displayMe);
        }

        [HttpPost("sundaes/submitEdit")]
        public IActionResult SubmitEdit(Sundae editedSundae)
        {
            Sundae editMe = _context.Sundaes
                .FirstOrDefault(sun => sun.SundaeId == editedSundae.SundaeId);

            editMe.Name = editedSundae.Name;
            editMe.Flavor = editedSundae.Flavor;
            editMe.Scoops = editedSundae.Scoops;
            editMe.Sauce = editedSundae.Sauce;
            editMe.Topping = editedSundae.Topping;

            _context.SaveChanges();

            return RedirectToAction("SingleSundae", new {id = editedSundae.SundaeId});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
