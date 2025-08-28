using Microsoft.AspNetCore.Mvc;
using TinderSwipe.Data;

namespace TinderSwipe.Controllers
{
    public class SignUpController : Controller
    {

        private readonly AppDbContext _context;
        public SignUpController(AppDbContext context)
        {
            _context = context;
        }

        //GET: SignUp

        public IActionResult Register()
        {
            return View();
        }
    }
}
