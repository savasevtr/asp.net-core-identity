using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyIdentity.Controllers
{
    [Authorize(Policy = "FemalePolicy")]
    public class FemaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
