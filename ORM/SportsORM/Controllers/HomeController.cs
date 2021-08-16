using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(banana => banana.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.ProblemOne = _context.Leagues
                .Where(league => league.Name.Contains("Women"))
                .ToList();

            //...all leagues where sport is any type of hockey
            ViewBag.ProblemTwo = _context.Leagues
                .Where(league => league.Sport.Contains("Hockey"))
                .ToList();

            ViewBag.ProblemFour = _context.Leagues
                .Where(league => league.Name.Contains("Conference"))
                .ToList();

            //...all teams based in Dallas
            ViewBag.ProblemSix = _context.Teams
                .Where(team => team.Location == "Dallas")
                .ToList();

            //...all teams, ordered alphabetically by location
            ViewBag.ProblemTen = _context.Teams
                .OrderBy(team => team.Location)
                .ToList();

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            //...all teams, past and present, that Samuel Evans has played with
            ViewBag.Sam = _context.Players
                .Include(player => player.CurrentTeam)
                .Include(player => player.AllTeams)
                .ThenInclude(pt => pt.TeamOfPlayer)
                .FirstOrDefault(player => player.FirstName == "Samuel" && player.LastName == "Evans");

            return View();
        }

    }
}