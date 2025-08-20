using Microsoft.AspNetCore.Mvc;
using TinderSwipe.Data;

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

        //POST: User/Like
        [HttpPost]
        public IActionResult Like(int id)
        {
            // Buscás el usuario por id
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Content("Usuario no encontrado 😢");
            }

            // Marcás el Like
            user.Like = true;

            // Guardás los cambios en la base de datos
            _context.SaveChanges();

            int currentIndex = (HttpContext.Session.GetInt32("UserIndex") ?? 0) + 1;
            HttpContext.Session.SetInt32("UserIndex", currentIndex);

            return RedirectToAction("Index");
        }

        //POST: User/Dislike
        [HttpPost]
        public IActionResult Dislike(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) 
            {
                return Content("Usuario no entonctrado 😢");
            }

            user.Like = false;
            
            _context.SaveChanges();

            int currentIndex = (HttpContext.Session.GetInt32("UserIndex") ?? 0) + 1;
            HttpContext.Session.SetInt32("UserIndex", currentIndex);

            return RedirectToAction("Index");

        }
    }
}

