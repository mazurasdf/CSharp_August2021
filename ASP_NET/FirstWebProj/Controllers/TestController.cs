using System;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebProj.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Hey it's me, the controller.....and I've changed!";
        }

        [HttpGet("another")]
        public string Another()
        {
            return "Hey it's me, the other route!";
        }

        [HttpGet("repeat/{phrase}/{times}")]
        public string Repeat(string phrase, int times)
        {
            Console.WriteLine(times);
            string newPhrase = phrase;
            for(int i = 0; i < times; ++i)
            {
                newPhrase += " " + phrase;
            }
            return newPhrase;
        }

        [HttpGet("page")]
        public IActionResult Main()
        {
            return View("Primary");
        }
    }
}