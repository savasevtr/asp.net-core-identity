using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyIdentity.Controllers
{
    [Authorize(Roles = "Developer")]
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}