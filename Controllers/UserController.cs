using Microsoft.AspNetCore.Mvc;
using TinderSwipe.Data;
using TinderSwipe.Models;

namespace TinderSwipe.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        //GET: User/Index
        public IActionResult Index() 
        {
            int currentIndex = HttpContext.Session.GetInt32("UserIndex") ?? 0;

            var users = _context.Users.ToList();
            
            if (currentIndex >= users.Count)
            {
                return Content("No hay más usuarios disponibles 😢");
            }

            var user = users[currentIndex];
            return View(user);

        }

        [HttpPost]
        public IActionResult Like(int id)
        {
            int currentIndex = HttpContext.Session.GetInt32("UserIndex") ?? 0;
            var users = _context.Users.ToList();
            var currentUser = users.ElementAtOrDefault(currentIndex);
            if (currentUser == null)
                return Content("No hay usuario actual 😢");

            // Crear registro de Like
            var like = new Like
            {
                UserId = currentUser.Id,
                LikeId = id
            };
            _context.Likes.Add(like);
            _context.SaveChanges();

            currentIndex++;
            HttpContext.Session.SetInt32("UserIndex", currentIndex);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Dislike(int id)
        {
            int currentIndex = HttpContext.Session.GetInt32("UserIndex") ?? 0;
            var users = _context.Users.ToList();
            var currentUser = users.ElementAtOrDefault(currentIndex);
            if (currentUser == null)
                return Content("No hay usuario actual 😢");

            // Crear registro de Dislike
            var like = new Like
            {
                UserId = currentUser.Id,
                DislikeId = id
            };
            _context.Likes.Add(like);
            _context.SaveChanges();

            currentIndex++;
            HttpContext.Session.SetInt32("UserIndex", currentIndex);
            return RedirectToAction("Index");
        }

    }
}

