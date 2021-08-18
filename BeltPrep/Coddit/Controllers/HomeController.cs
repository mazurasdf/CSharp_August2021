using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coddit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Coddit.Controllers
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
                if(_context.Users.Any(user => user.UserName == newUser.UserName))
                {
                    ModelState.AddModelError("UserName", "UserName already in use!!!!!!!");

                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost("checkLogin")]
        public IActionResult CheckLogin(LoginUser login)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(user => user.UserName == login.LoginUserName);

                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginUserName", "Invalid login");

                    return View("Index");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.LoginPassword);

                if(result == 0)
                {
                    ModelState.AddModelError("LoginUserName", "Invalid login");

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
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            
            ViewBag.LoggedUser = _context.Users.FirstOrDefault(user => user.UserId == loggedUserId);
            ViewBag.AllPosts = _context.Posts
                .Include(post => post.Creator)
                .Include(post => post.Likes)
                .Include(post => post.CommentsMade)
                .ToList();

            return View();
        }

        [HttpGet("posts/new")]
        public IActionResult PostForm()
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            return View();
        }

        [HttpPost("posts/submit")]
        public IActionResult SubmitPost(Post newPost)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            if(ModelState.IsValid)
            {
                newPost.UserId = (int)loggedUserId;
                _context.Add(newPost);
                _context.SaveChanges();

                return RedirectToAction("Main");
            }
            return View("PostForm");
        }

        [HttpGet("posts/{id}")]
        public IActionResult SinglePost(int id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            ViewBag.LoggedUser = _context.Users.FirstOrDefault(user => user.UserId == loggedUserId);
            ViewBag.SinglePost = _context.Posts
                .Include(post => post.Creator)
                .Include(post=> post.CommentsMade)
                .ThenInclude(comment => comment.Creator)
                .Include(post => post.Likes)
                .FirstOrDefault(post => post.PostId == id);

            return View();
        }

        [HttpPost("addComment")]
        public IActionResult AddComment(Comment newComment)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            if(ModelState.IsValid)
            {
                newComment.UserId = (int)loggedUserId;
                _context.Add(newComment);
                _context.SaveChanges();

                return RedirectToAction("SinglePost", new {id=newComment.PostId});
            }

            ViewBag.LoggedUser = _context.Users.FirstOrDefault(user => user.UserId == loggedUserId);
            ViewBag.SinglePost = _context.Posts
                .Include(post => post.Creator)
                .Include(post=> post.CommentsMade)
                .ThenInclude(comment => comment.Creator)
                .Include(post => post.Likes)
                .FirstOrDefault(post => post.PostId == newComment.PostId);

            return View("SinglePost");
        }

        [HttpGet("posts/{postId}/like")]
        public IActionResult AddLike(int postId)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            Like checkForLike = _context.Likes
                .FirstOrDefault(lk => lk.UserId ==(int)loggedUserId && lk.PostId == postId);

            if(checkForLike == null)
            {
                Console.WriteLine("like will be added");
                Like newLike = new Like();
                newLike.UserId = (int)loggedUserId;
                newLike.PostId = postId;

                _context.Add(newLike);
                _context.SaveChanges();
            }
            else
            {
                _context.Likes.Remove(checkForLike);
                _context.SaveChanges();
            }
            

            return RedirectToAction("SinglePost", new {id=postId});
        }

        [HttpGet("posts/topic/{topic}")]
        public IActionResult TopicPage(string topic)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            
            ViewBag.LoggedUser = _context.Users.FirstOrDefault(user => user.UserId == loggedUserId);
            ViewBag.AllPosts = _context.Posts
                .Include(post => post.Creator)
                .Include(post => post.Likes)
                .Include(post => post.CommentsMade)
                .Where(post => post.Topic == topic)
                .ToList();

            return View();
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
