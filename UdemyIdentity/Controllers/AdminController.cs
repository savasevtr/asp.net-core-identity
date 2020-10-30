using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyIdentity.Controllers
{
    [Authorize(Roles = "Admin,Developer")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}