using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TinderSwipe.Data;
using TinderSwipe.ViewModels;
using TinderSwipe.Models;
using System.Linq;



namespace TinderSwipe.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        //POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            // Busca coincidencias en la base de datos
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            // Si no encuentra al usuario, devuelve un mensaje de error en la misma vista
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View(model);
            }

            // Guarda el ID del usuario en sesión
            HttpContext.Session.SetInt32("UserId", user.Id);

            // Redirige al returnUrl si existe, o al Home/Index si no
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
