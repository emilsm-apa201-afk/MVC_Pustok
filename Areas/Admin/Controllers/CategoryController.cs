using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.Areas.Admin.ViewModels;
using MVC_Pustokkk.DAL;
using MVC_Pustokkk.Models;

namespace MVC_Pustokkk.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Include(x=>x.Featureds).ToList();
            List<CategoryVM> VMs = new List<CategoryVM>();
            
            foreach (var category in categories)
            {
                VMs.Add(new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                    Featureds = category.Featureds.Select(f => new FeaturedVM()
                    {
                        Id = f.Id,
                        Author = f.Author,
                        Details = f.Details,
                        Image = f.Image,
                        Price = f.Price,
                        PriceOld = f.PriceOld,
                        PriceDiscount = f.PriceDiscount
                    }).ToList()
                }); 
            }

            return View(VMs);
        }

        public IActionResult Detail(int? id)
        {
            if (!id.HasValue || id < 1)
            {
                return BadRequest();
            }

            Category? category = _context.Categories.Include(c=>c.Featureds).FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                Featureds = category.Featureds.Select(f => new FeaturedVM()
                {
                    Id = f.Id,
                    Author = f.Author,
                    Details = f.Details,
                    Image = f.Image,
                    Price = f.Price,
                    PriceOld = f.PriceOld,
                    PriceDiscount = f.PriceDiscount
                }).ToList()
            });              
        }
    }
}
