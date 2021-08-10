using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hangman.Models;
using Microsoft.AspNetCore.Http;

namespace Hangman.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("submitWord")]
        public IActionResult SubmitWord(string word)
        {
            Console.WriteLine(word);
            HttpContext.Session.SetString("Answer", word);
            //set session with: img URL, correct guesses, incorrect guesses
            HttpContext.Session.SetString("imgURL", "~/img/0.png");
            string underscores = "";
            for(int i = 0; i < word.Length; ++i)
            {
                underscores += "_";
            }
            HttpContext.Session.SetString("Correct", underscores);
            HttpContext.Session.SetString("incorrect", "");

            return RedirectToAction("Game");
        }

        [HttpGet("game")]
        public IActionResult Game()
        {
            ViewBag.Correct = HttpContext.Session.GetString("Correct");
            ViewBag.Incorrect = HttpContext.Session.GetString("incorrect");
            ViewBag.imgURL = Url.Content(HttpContext.Session.GetString("imgURL"));
            ViewBag.Answer = HttpContext.Session.GetString("Answer");

            if(HttpContext.Session.GetString("Answer") == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost("guessLetter")]
        public IActionResult GuessLetter(Guess guess)
        {   
            if(!ModelState.IsValid)
            {
                ViewBag.Correct = HttpContext.Session.GetString("Correct");
                ViewBag.Incorrect = HttpContext.Session.GetString("incorrect");
                ViewBag.imgURL = Url.Content(HttpContext.Session.GetString("imgURL"));
                return View("Game");
            }

            string answer = HttpContext.Session.GetString("Answer");
            string oldCorrect = HttpContext.Session.GetString("Correct");            
            string incorrect = HttpContext.Session.GetString("incorrect");

            Console.WriteLine(answer);
            if(answer.Contains(guess.Letter))
            {
                string correct = "";

                for(int i = 0; i < answer.Length; ++i)
                {
                    if(answer[i] == guess.Letter[0])
                    {
                        correct += guess.Letter;
                    }
                    else
                    {
                        correct += oldCorrect[i];
                    }
                }

                if(correct == answer)
                {
                    HttpContext.Session.SetString("imgURL", "~/img/win.png");
                }

                HttpContext.Session.SetString("Correct", correct);
            }
            else
            {
                incorrect += guess.Letter;
                HttpContext.Session.SetString("incorrect", incorrect);

                string newURL = $"~/img/{incorrect.Length}.png";
                HttpContext.Session.SetString("imgURL", newURL);
            }

            return RedirectToAction("Game");
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
