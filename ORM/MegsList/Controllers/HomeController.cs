using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MegsList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MegsList.Controllers
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
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!!!!!!!");
                    Console.WriteLine("email found");
                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            Console.WriteLine("model not valid");
            return View("Index");
        }

        [HttpPost("checkLogin")]
        public IActionResult CheckLogin(LoginUser login)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(user => user.Email == login.LoginEmail);

                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid login");

                    return View("Index");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.LoginPassword);

                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid login");

                    return View("Index");
                }

                Console.WriteLine("logged in");
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                return RedirectToAction("Main");
            }

            return View("Index");
        }

        [HttpGet("main")]
        public IActionResult Main()
        {
            int? loggedId = HttpContext.Session.GetInt32("userId");
            if(loggedId == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.AllListings = _context.Listings
                .Include(lst => lst.Seller)
                .ToList();
            return View();
        }

        [HttpGet("addListing")]
        public IActionResult AddListingPage()
        {
            int? loggedId = HttpContext.Session.GetInt32("userId");
            if(loggedId == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("submitListing")]
        public IActionResult SubmitListing(Listing newListing)
        {
            int? loggedId = HttpContext.Session.GetInt32("userId");
            if(loggedId == null)
            {
                return RedirectToAction("Index");
            }
            if(ModelState.IsValid)
            {
                newListing.UserId = (int)loggedId;
                _context.Listings.Add(newListing);
                _context.SaveChanges();
                return RedirectToAction("Main");
            }
            return View("AddListingPage");
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
