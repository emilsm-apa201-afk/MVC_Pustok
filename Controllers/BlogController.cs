using Microsoft.AspNetCore.Mvc;

namespace MVC_Pustokkk.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
