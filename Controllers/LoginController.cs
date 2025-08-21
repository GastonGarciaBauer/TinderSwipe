using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinderSwipe.Data;
using TinderSwipe.Models;

namespace TinderSwipe.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }



    }
}
