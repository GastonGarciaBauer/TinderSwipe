using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //POST: Like
        [HttpPost]
        
        public IActionResult Like(int id)
        {

            //Obtengo la sesión del usuario actual
            int currentIndex = HttpContext.Session.GetInt32("UserIndex") ?? 0;
            
            //Obtengo lista de todos los usuarios
            var users = _context.Users.ToList();
            
            //Obtengo el usuario que coincida con el nro de sesión actual
            var currentUser = users.ElementAtOrDefault(currentIndex);

            if (currentUser == null) 
            {
                return Content("Por el momento no hay más usuarios cerca tuyo... 😢");
            }

            // Instancio un objeto de tipo Like (tabla de BD), para asignarle valores al registro
            var like = new Like
            {
                UserId = currentUser.Id,
                LikeId = id
            };

            // Asigno valores y guardo
            _context.Likes.Add(like);
            _context.SaveChanges();

            //Sumo 1 a la sesión actual para que busque al siguiente usuario
            currentIndex++;
            //Seteo la nueva sesión
            HttpContext.Session.SetInt32("UserIndex", currentIndex);

            // Redirijo al index
            return RedirectToAction("Index");
        }

        public IActionResult Dislike(int id)
        {
            int currentIndex = HttpContext.Session.GetInt32("UserIndex") ?? 0;
            
            var users = _context.Users.ToList();

            var currentUser = users.ElementAtOrDefault(currentIndex);

            if (currentUser == null) 
            {
                return Content("Por el momento no hay más usuarios cerca tuyo... 😢");
            }

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
