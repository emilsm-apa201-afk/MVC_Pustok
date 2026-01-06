using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.DAL;
using MVC_Pustokkk.Models;
using MVC_Pustokkk.ViewModels;

namespace MVC_Pustokkk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;


        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Featureds = await _context.Featureds.ToListAsync()
            };
            return View(vm);
        }

    }
}
