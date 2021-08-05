using System;
using Microsoft.AspNetCore.Mvc;

namespace ChooseYourOwnAdventure.Controllers
{
    public class AdventureController : Controller
    {
        [HttpGet("")]
        public IActionResult Main()
        {
            return View();
        }

        [HttpGet("door/{num}")]
        public IActionResult Room(int num)
        {
            if(num == 1)
            {
                ViewBag.title = "sorry, you ded";
                ViewBag.door1Text = "you really messed up here, click to move on";
                ViewBag.door1Link = "/";
            }
            else if(num == 2)
            {
                ViewBag.title = "Ahhhh a true riddle lover!!!! Pick the correct door to move on!";
                ViewBag.door1Text = "Door 3";
                ViewBag.door1Link = "/door/3";
                ViewBag.door2Text = "Door 4";
                ViewBag.door2Link = "/door/4";
            }
            else if(num == 3)
            {
                return RedirectToAction("Final");
            }
            else if(num == 4)
            {
                ViewBag.title = "You walked into the door without grabbing the handle. The shock and surprise stunned you so much, you fell backwardas and hit your head, killing you immediately.";
                ViewBag.door1Text = "you really messed up here, click to move on";
                ViewBag.door1Link = "/";
            }
            else if(num == 5)
            {
                ViewBag.title = "nice";
                ViewBag.door1Text = "you win! play again?";
                ViewBag.door1Link = "/";
            }
            else
            {
                ViewBag.title = "The old man cackles. 'You simpletons will never learn!!!' He stands up and reveals his old wrinkly hands  as they stretch towards you. He finally screams out 'UNLIMITED POWER' as lightning bolts shoot from his fingertips and disintegrate you immediately.";
                ViewBag.door1Text = "you ded! play again?";
                ViewBag.door1Link = "/";
            }
            return View();
        }

        [HttpGet("final")]
        public IActionResult Final()
        {
            return View("Last");
        }

        [HttpPost("submitAnswer")]
        public IActionResult SubmitAnswer(int answer)
        {
            if(answer == 5)
            {
                return RedirectToAction("Room", new {num = 5});
            }
            else
            {
                return RedirectToAction("Room", new {num = 6});
            }  
        }
    }
}