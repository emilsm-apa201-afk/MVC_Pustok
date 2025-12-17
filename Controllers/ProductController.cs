using Microsoft.AspNetCore.Mvc;

namespace MVC_Pustokkk.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
